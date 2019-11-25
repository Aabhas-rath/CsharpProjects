package com.topcoder.quartzenergy.mudlog.search.repository;

import com.topcoder.quartzenergy.mudlog.search.dto.*;
import lombok.extern.slf4j.Slf4j;
import org.apache.commons.compress.utils.Lists;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.dao.DataAccessException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.ResultSetExtractor;
import org.springframework.jdbc.core.namedparam.MapSqlParameterSource;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;
import org.springframework.util.StringUtils;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.*;

import static com.topcoder.quartzenergy.mudlog.search.utils.MudlogUtils.safeDecode;

@Slf4j
@Repository
public class WellRepository {

    /**
     * the jdbc template instance
     */
    private JdbcTemplate jdbcTemplate;

    /**
     * the named parameters jdbc template instance
     */
    private NamedParameterJdbcTemplate namedJdbcTemplate;

    /**
     * identifier to flag user selected All States from states dropdown
     */
    private static String ALL_STATES_IDENTIFIER = "all-states";

    /**
     * Query for retrieving wells in a bounding box given by lat and long
     */
    String GET_ALL_WELL_QUERY = "SELECT wi.UWI," +
            "          wi.WELL_NAME," +
            "          wi.PROVINCE_STATE_NAME," +
            "          wi.COUNTRY_NAME," +
            "          YEAR(wi.SPUD_DATE) as SPUD_DATE," +
            "          wi.OPERATOR_NAME," +
            "          wi.BASIN_NAME," +
            "          iocr.IMAGE_URL," +
            "          iocr.SCORE," +
            "         wll.LATITUDE, wll.LONGITUDE" +
            "   FROM well_info AS wi" +
            "   LEFT JOIN IMAGE_OCR iocr ON wi.UWI = iocr.UWI" +
            "   LEFT JOIN uwi_lat_long wll ON wi.UWI = wll.UWI " +
            "   WHERE wll.LATITUDE > :minLat AND wll.LATITUDE < :maxLat AND " +
            "   wll.LONGITUDE > :minLong AND wll.LONGITUDE < :maxLong " +
            "   ORDER BY wi.WELL_NAME ASC";

    /**
     * Query for finding matching phrases count
     */
    String MATCHING_PHRASES_COUNT_TABLE = "SELECT wi.UWI, " +
            "          count(*) AS phrases_count " +
            "   FROM well_info AS wi " +
            "   LEFT JOIN IMAGE_OCR iocr ON wi.UWI = iocr.UWI " +
            "   LEFT JOIN IMAGE_OCR_PHRASE iop ON iocr.IMAGE_URL = iop.IMAGE_URL " +
            "   WHERE iop.OCR_PHRASE in (:phrasesList)" +
            "   GROUP BY wi.UWI";

    /**
     * Query for finding well count
     */
    String FILTERED_WELL_COUNT_SQL = "SELECT count(DISTINCT wi.UWI) " +
            "FROM well_info AS wi " +
            "LEFT JOIN IMAGE_OCR iocr ON wi.UWI = iocr.UWI " +
            "LEFT JOIN" +
            "  (SELECT iod.IMAGE_URL, max(iod.[DEPTH]) AS [DEPTH]" +
            "   FROM IMAGE_OCR_DEPTH iod" +
            "   WHERE iod.[DEPTH] >= :minDepth" +
            "     AND iod.[DEPTH] <= :maxDepth" +
            "   GROUP BY iod.IMAGE_URL) iod ON iocr.IMAGE_URL = iod.IMAGE_URL " +
            "LEFT JOIN uwi_lat_long wll ON wi.UWI = wll.UWI ";

    /**
     * Query for search well
     */
    String WELL_SEARCH_SQL = "SELECT wi.UWI, wi.WELL_NAME, wi.OPERATOR_NAME, " +
            "wi.PROVINCE_STATE_NAME, wi.COUNTRY_NAME, wi.FIELD_NAME, wi.BASIN_NAME," +
            "wi.OPERATOR_NAME, YEAR(wi.SPUD_DATE) as SPUD_DATE, wll.LATITUDE, wll.LONGITUDE," +
            "icp.ALIAS, icp.OCR_PHRASE, paginated.[DEPTH], icp.ESTIMATED_DEPTH, iocr.IMAGE_URL, " +
            "iocr.IMAGE_NAME, iocr.PHRASE_COUNT, iocr.SCORE " +
            "FROM" +
            "  (SELECT distinct wi.UWI," +
            "          wi.WELL_NAME, iod.[DEPTH] " +
            "   FROM well_info AS wi" +
            "   LEFT JOIN IMAGE_OCR iocr ON wi.UWI = iocr.UWI" +
            "   LEFT JOIN" +
            "  (SELECT iod.IMAGE_URL, max(iod.[DEPTH]) AS [DEPTH]" +
            "   FROM IMAGE_OCR_DEPTH iod" +
            "   WHERE iod.[DEPTH] >= :minDepth" +
            "     AND iod.[DEPTH] <= :maxDepth" +
            "           GROUP BY iod.IMAGE_URL) iod ON iocr.IMAGE_URL = iod.IMAGE_URL " +
            "   LEFT JOIN uwi_lat_long wll on wll.UWI = wi.UWI " +
            "   %PHRASE_JOIN% " +
            "   WHERE %WHERE_CLAUSE% " +
            "   ORDER BY wi.WELL_NAME ASC" +
            "   OFFSET :offset ROWS FETCH NEXT :pagesize ROWS ONLY) paginated " +
            "LEFT JOIN well_info wi ON paginated.UWI = wi.UWI " +
            "LEFT JOIN IMAGE_OCR iocr ON paginated.UWI = iocr.UWI " +
            "LEFT JOIN IMAGE_OCR_PHRASE icp ON iocr.IMAGE_URL = icp.IMAGE_URL " +
            "LEFT JOIN uwi_lat_long wll on wll.UWI = wi.UWI ";

    /**
     * Query for searching ocr phrase
     */
    String OCR_PHRASE_SEARCH_SQL = "SELECT  icp.IMAGE_URL, icp.IMAGE_NAME, icp.OCR_PHRASE_TYPE, " +
            "icp.OCR_PHRASE, icp.SCORE, icp.X1, icp.Y1, icp.X2, icp.Y2, icp.ESTIMATED_DEPTH, " +
            "icp.PHASE_KEY, icp.REF_KEY " +
            "FROM" +
            "  (SELECT distinct wi.UWI," +
            "          wi.WELL_NAME " +
            "   FROM well_info AS wi" +
            "   LEFT JOIN IMAGE_OCR iocr ON wi.UWI = iocr.UWI" +
            "   LEFT JOIN (SELECT iod.IMAGE_URL, max(iod.[DEPTH]) AS [DEPTH]" +
            "             FROM IMAGE_OCR_DEPTH iod" +
            "             WHERE iod.[DEPTH] >= :minDepth AND iod.[DEPTH] <= :maxDepth" +
            "             GROUP BY iod.IMAGE_URL) iod " +
            "   ON iocr.IMAGE_URL = iod.IMAGE_URL" +
            "   LEFT JOIN uwi_lat_long wll on wll.UWI = wi.UWI " +
            "   %PHRASE_JOIN% " +
            "   WHERE %WHERE_CLAUSE% ) filtered " +
            "LEFT JOIN well_info wi ON filtered.UWI = wi.UWI " +
            "LEFT JOIN IMAGE_OCR iocr ON filtered.UWI = iocr.UWI " +
            "LEFT JOIN IMAGE_OCR_PHRASE icp ON iocr.IMAGE_URL = icp.IMAGE_URL " +
            "LEFT JOIN uwi_lat_long wll on wll.UWI = wi.UWI " +
            "WHERE icp.IMAGE_NAME is NOT NULL and icp.IMAGE_URL is not NULL " +
            "ORDER BY icp.IMAGE_NAME ASC";

    /**
     * Autowired constructor
     *
     * @param jdbcTemplate the jdbc template instance
     */
    @Autowired
    public WellRepository(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
        this.namedJdbcTemplate = new NamedParameterJdbcTemplate(jdbcTemplate.getDataSource());
    }

    /**
     * get all wells
     *
     * @param minLat  latitude of top left corner of a rectangle
     * @param maxLat  latitude of bottom right corner of a rectangle
     * @param minLong longitude of top left corner of a rectangle
     * @param maxLong longitude of top bottom right of a rectangle
     * @return list of found Wells
     */
    @Transactional(readOnly = true)
    public List<WellInfo> getAllWells(Double minLat, Double maxLat, Double minLong, Double maxLong) {
        log.info("Get all well query is: " + GET_ALL_WELL_QUERY);
        MapSqlParameterSource parameters = new MapSqlParameterSource();
        parameters.addValue("minLat", minLat);
        parameters.addValue("maxLat", maxLat);
        parameters.addValue("minLong", minLong);
        parameters.addValue("maxLong", maxLong);
        return namedJdbcTemplate.query(GET_ALL_WELL_QUERY, parameters, new WellInfoResultSetExtractor());
    }

    /**
     * Find number of wells found by given set of parameters
     *
     * @param uwis           list of UWI to search
     * @param states         list of states to search
     * @param basin          basin to search
     * @param county         county to search
     * @param field          field to search
     * @param minDepth       min depth of the well
     * @param maxDepth       max depth of the well
     * @param minVintage     min vintage year
     * @param maxVintage     max vintage year
     * @param phrases        list of phrases to search
     * @param minPhraseCount min phrase count
     * @param maxPhraseCount max phrase count
     * @param minPhraseScore min phrase score
     * @param maxPhraseScore max phrase score
     * @param minLat         latitude of top left corner of a rectangle
     * @param maxLat         latitude of bottom right corner of a rectangle
     * @param minLong        longitude of top left corner of a rectangle
     * @param maxLong        longitude of top bottom right of a rectangle
     * @return list of found Wells
     */
    @Transactional(readOnly = true)
    public Long searchWellCount(List<String> uwis, List<String> states, String basin, String county, String field,
                                int minDepth, int maxDepth, int minVintage, int maxVintage,
                                List<String> phrases, int minPhraseCount, int maxPhraseCount,
                                int minPhraseScore, int maxPhraseScore, Double minLat, Double maxLat, Double minLong, Double maxLong) {
        MapSqlParameterSource parameters = new MapSqlParameterSource();
        List<String> wellWhereClauses = new ArrayList<>();
        appendColumnQueryParts(wellWhereClauses, parameters, uwis, states, basin, county, field, minVintage, maxVintage);
        appendOcrRelatedParts(wellWhereClauses, parameters, minDepth, maxDepth, minPhraseCount, maxPhraseCount, minPhraseScore, maxPhraseScore);
        appendMapExtentRelatedParts(wellWhereClauses, parameters, minLat, maxLat, minLong, maxLong);

        String wellCountQuery = FILTERED_WELL_COUNT_SQL;
        if (phrases != null && phrases.size() > 0) {
            wellCountQuery += "LEFT JOIN (" + MATCHING_PHRASES_COUNT_TABLE + ") phrases ON phrases.UWI = wi.UWI ";
            parameters.addValue("phrasesList", phrases);
            wellWhereClauses.add("phrases.phrases_count > 0");
        }
        wellCountQuery += " WHERE " + StringUtils.collectionToDelimitedString(wellWhereClauses, " AND ");
        log.info("Well count query is: " + wellCountQuery);
        return namedJdbcTemplate.queryForObject(wellCountQuery,
                parameters, Long.class);
    }

    /**
     * Search wells by given set of parameters
     *
     * @param uwis           list of UWI to search
     * @param states         list of states to search
     * @param basin          basin to search
     * @param county         county to search
     * @param field          field to search
     * @param minDepth       min depth of the well
     * @param maxDepth       max depth of the well
     * @param minVintage     min vintage year
     * @param maxVintage     max vintage year
     * @param phrases        list of phrases to search
     * @param minPhraseCount min phrase count
     * @param maxPhraseCount max phrase count
     * @param minPhraseScore min phrase score
     * @param maxPhraseScore max phrase score
     * @param minLat         latitude of top left corner of a rectangle
     * @param maxLat         latitude of bottom right corner of a rectangle
     * @param minLong        longitude of top left corner of a rectangle
     * @param maxLong        longitude of top bottom right of a rectangle
     * @param offset         list of rows to skip
     * @param limit          list of rows to return
     * @return list of found Wells
     */
    @Transactional(readOnly = true)
    public List<FilteredWell> searchWells(List<String> uwis, List<String> states, String basin, String county, String field,
                                          int minDepth, int maxDepth, int minVintage, int maxVintage,
                                          List<String> phrases, int minPhraseCount, int maxPhraseCount,
                                          int minPhraseScore, int maxPhraseScore, Double minLat, Double maxLat, Double minLong,
                                          Double maxLong, int offset, int limit) {
        MapSqlParameterSource parameters = new MapSqlParameterSource();
        List<String> wellWhereClauses = new ArrayList<>();
        appendColumnQueryParts(wellWhereClauses, parameters, uwis, states, basin, county, field, minVintage, maxVintage);
        appendOcrRelatedParts(wellWhereClauses, parameters, minDepth, maxDepth, minPhraseCount, maxPhraseCount, minPhraseScore, maxPhraseScore);
        appendMapExtentRelatedParts(wellWhereClauses, parameters, minLat, maxLat, minLong, maxLong);
        parameters.addValue("offset", offset);
        parameters.addValue("pagesize", limit);

        String searchWellQuery = WELL_SEARCH_SQL;
        if (phrases != null && phrases.size() > 0) {
            searchWellQuery = searchWellQuery.replaceAll("%PHRASE_JOIN%",
                    "LEFT JOIN (" + MATCHING_PHRASES_COUNT_TABLE + ") phrases ON phrases.UWI = wi.UWI ");
            parameters.addValue("phrasesList", phrases);
            wellWhereClauses.add("phrases.phrases_count >= 0");
        } else {
            searchWellQuery = searchWellQuery.replaceAll("%PHRASE_JOIN%", "");
        }
        searchWellQuery = searchWellQuery.replaceAll("%WHERE_CLAUSE%",
                StringUtils.collectionToDelimitedString(wellWhereClauses, " AND "));

        log.info("Well search query is: " + searchWellQuery);
        return namedJdbcTemplate.query(searchWellQuery,
                parameters, new WellResultSetExtractor());
    }

    /**
     * Search and export wells found by applying given filters
     *
     * @param uwis           list of UWI to search
     * @param states         list of states to search
     * @param basin          basin to search
     * @param county         county to search
     * @param field          field to search
     * @param minDepth       min depth of the well
     * @param maxDepth       max depth of the well
     * @param minVintage     min vintage year
     * @param maxVintage     max vintage year
     * @param phrases        list of phrases to search
     * @param minPhraseCount min phrase count
     * @param maxPhraseCount max phrase count
     * @param minPhraseScore min phrase score
     * @param maxPhraseScore max phrase score
     * @param minLat         latitude of top left corner of a rectangle
     * @param maxLat         latitude of bottom right corner of a rectangle
     * @param minLong        longitude of top left corner of a rectangle
     * @param maxLong        longitude of top bottom right of a rectangle
     * @return list of found raw phrase data
     */
    @Transactional(readOnly = true)
    public List<RawPhraseData> searchAndGetRawPhraseData(List<String> uwis, List<String> states, String basin,
                                                         String county, String field, int minDepth, int maxDepth,
                                                         int minVintage, int maxVintage, List<String> phrases,
                                                         int minPhraseCount, int maxPhraseCount, int minPhraseScore,
                                                         int maxPhraseScore, Double minLat, Double maxLat,
                                                         Double minLong, Double maxLong) {
        MapSqlParameterSource parameters = new MapSqlParameterSource();
        List<String> wellWhereClauses = new ArrayList<>();
        appendColumnQueryParts(wellWhereClauses, parameters, uwis, states, basin, county, field, minVintage, maxVintage);
        appendOcrRelatedParts(wellWhereClauses, parameters, minDepth, maxDepth, minPhraseCount, maxPhraseCount, minPhraseScore, maxPhraseScore);
        appendMapExtentRelatedParts(wellWhereClauses, parameters, minLat, maxLat, minLong, maxLong);

        String phraseRawDataSearchQuery = OCR_PHRASE_SEARCH_SQL;
        if (phrases != null && phrases.size() > 0) {
            phraseRawDataSearchQuery = phraseRawDataSearchQuery.replaceAll("%PHRASE_JOIN%",
                    "LEFT JOIN (" + MATCHING_PHRASES_COUNT_TABLE + ") phrases ON phrases.UWI = wi.UWI ");
            parameters.addValue("phrasesList", phrases);
            wellWhereClauses.add("phrases.phrases_count >= 0");
        } else {
            phraseRawDataSearchQuery = phraseRawDataSearchQuery.replaceAll("%PHRASE_JOIN%", "");
        }
        phraseRawDataSearchQuery = phraseRawDataSearchQuery.replaceAll("%WHERE_CLAUSE%",
                StringUtils.collectionToDelimitedString(wellWhereClauses, " AND "));

        log.info("Phrase raw data search query is: " + phraseRawDataSearchQuery);
        return namedJdbcTemplate.query(phraseRawDataSearchQuery,
                parameters, new OcrPhraseResultSetExtractor());
    }

    /**
     * append well related columns to where claus and parameters list to be used by select query
     *
     * @param whereClauses existing list of where clause parts
     * @param parameters   existing parameters map
     * @param uwis         the apis
     * @param states       the states
     * @param basin        the basin
     * @param county       the county
     * @param field        the field
     * @param minVintage   the min vintage year
     * @param maxVintage   the max vintage year
     */
    private void appendColumnQueryParts(List<String> whereClauses, MapSqlParameterSource parameters,
                                        List<String> uwis, List<String> states,
                                        String basin, String county, String field,
                                        int minVintage, int maxVintage) {
        if (uwis != null && uwis.size() > 0) {
            whereClauses.add("wi.UWI in (:uwis)");
            parameters.addValue("uwis", uwis);
        }

        if (!StringUtils.isEmpty(field)) {
            whereClauses.add("wi.FIELD_NAME = :fieldName");
            parameters.addValue("fieldName", field);
        } else if (!StringUtils.isEmpty(county)) {
            whereClauses.add("wi.COUNTY_NAME = :countyName");
            parameters.addValue("countyName", county);
        } else if (!StringUtils.isEmpty(basin)) {
            whereClauses.add("wi.BASIN_NAME = :basinName");
            parameters.addValue("basinName", basin);
        } else if (states != null && states.size() > 0 && !states.contains(ALL_STATES_IDENTIFIER)) {
            whereClauses.add("wi.PROVINCE_STATE_NAME in (:stateNames)");
            parameters.addValue("stateNames", states);
        }

        whereClauses.add("YEAR(wi.SPUD_DATE) >= :minVintage");
        whereClauses.add("YEAR(wi.SPUD_DATE) <= :maxVintage");
        parameters.addValue("minVintage", minVintage);
        parameters.addValue("maxVintage", maxVintage);

    }

    /**
     * append ocr related column related part to the where clause and parameters map
     *
     * @param whereClauses   existing where clause parts
     * @param parameters     existing parameters map
     * @param minDepth       the min depth
     * @param maxDepth       max depth
     * @param minPhraseCount min phrase count
     * @param maxPhraseCount max phrase count
     * @param minPhraseScore min phrase score
     * @param maxPhraseScore max phrase score
     */
    private void appendOcrRelatedParts(List<String> whereClauses, MapSqlParameterSource parameters,
                                       int minDepth, int maxDepth, int minPhraseCount, int maxPhraseCount,
                                       int minPhraseScore, int maxPhraseScore) {

        whereClauses.add("iocr.PHRASE_COUNT >= :minPhraseCount");
        whereClauses.add("iocr.PHRASE_COUNT <= :maxPhraseCount");
        whereClauses.add("iocr.SCORE >= :minPhraseScore");
        whereClauses.add("iocr.SCORE <= :maxPhraseScore");

        parameters.addValue("minDepth", minDepth);
        parameters.addValue("maxDepth", maxDepth);
        parameters.addValue("minPhraseCount", minPhraseCount);
        parameters.addValue("maxPhraseCount", maxPhraseCount);
        parameters.addValue("minPhraseScore", minPhraseScore);
        parameters.addValue("maxPhraseScore", maxPhraseScore);
    }

    /**
     * append lat, long related columns info to where clause and parameters map
     *
     * @param whereClauses existing where clause parts
     * @param parameters   existing parameters map
     * @param minLat       min latitude
     * @param maxLat       max latitude
     * @param minLong      min longitude
     * @param maxLong      max longitude
     */
    private void appendMapExtentRelatedParts(List<String> whereClauses, MapSqlParameterSource parameters,
                                             Double minLat, Double maxLat, Double minLong, Double maxLong) {
        if (minLat == null || maxLat == null || minLong == null || maxLong == null) {
            return;
        }
        whereClauses.add("wll.LATITUDE > :minLat");
        whereClauses.add("wll.LATITUDE < :maxLat");
        whereClauses.add("wll.LONGITUDE > :minLong");
        whereClauses.add("wll.LONGITUDE < :maxLong");

        parameters.addValue("minLat", minLat);
        parameters.addValue("maxLat", maxLat);
        parameters.addValue("minLong", minLong);
        parameters.addValue("maxLong", maxLong);
    }

    /**
     * Mapper for mapping resultset rows to a Well info
     */
    class WellInfoResultSetExtractor implements ResultSetExtractor<List<WellInfo>> {

        /**
         * @param rs the resultset
         * @return mapped instance of WellInfo
         * @throws SQLException in case of any exception
         */
        @Override
        public List<WellInfo> extractData(ResultSet rs) throws SQLException, DataAccessException {
            Map<String, WellInfo> uwiAndWells = new HashMap<>();
            while (rs.next()) {
                String uwi = rs.getString("UWI");
                String wellName = rs.getString("WELL_NAME");
                String stateName = rs.getString("PROVINCE_STATE_NAME");
                String countryName = rs.getString("COUNTRY_NAME");
                int vintage = rs.getInt("SPUD_DATE");
                String imageUrl = rs.getString("IMAGE_URL");
                String operator = rs.getString("OPERATOR_NAME");
                String basinName = rs.getString("BASIN_NAME");
                int score = rs.getInt("SCORE");
                double lat = rs.getDouble("LATITUDE");
                double lng = rs.getDouble("LONGITUDE");
                WellInfo wellInfo = uwiAndWells.get(uwi);

                if(wellInfo != null) {
                    //existing well with uwi, add the image URL
                    if(wellInfo.getImageUrls() == null) {
                        wellInfo.setImageUrls(new ArrayList<>());
                    }
                } else {
                    wellInfo = WellInfo.builder().api(uwi).name(wellName)
                            .place(WellPlace.builder()
                                    .lat(lat)
                                    .lng(lng)
                                    .location(stateName + ", " + countryName)
                                    .build())
                            .phraseScore(score)
                            .operator(operator)
                            .vintage(vintage)
                            .basin(basinName)
                            .imageUrls(Lists.newArrayList())
                            .build();
                    uwiAndWells.put(uwi, wellInfo);
                }
                wellInfo.getImageUrls().add(safeDecode(imageUrl));
                wellInfo.getImageUrls().sort(String::compareTo);
            }
            return new ArrayList<>(uwiAndWells.values());


        }
    }

    /**
     * Mapper for mapping resultset rows to a FilteredWell
     */
    class WellResultSetExtractor implements ResultSetExtractor<List<FilteredWell>> {

        /**
         * map resultset row into FilteredWell
         *
         * @param rs the resultset
         * @return list of mapped filteredWells
         * @throws SQLException        in case of any SQL errors
         * @throws DataAccessException in case of any DataAccessException
         */
        @Override
        public List<FilteredWell> extractData(ResultSet rs) throws SQLException, DataAccessException {
            List<FilteredWell> wellList = new ArrayList<>();
            Map<String, FilteredWell> uwiAndWells = new HashMap<>();
            int count = 0;
            while (rs.next()) {
                count++;
                String uwi = rs.getString("UWI");
                String wellName = rs.getString("WELL_NAME");
                String stateName = rs.getString("PROVINCE_STATE_NAME");
                String countryName = rs.getString("COUNTRY_NAME");
                String operatorName = rs.getString("OPERATOR_NAME");
                int vintage = rs.getInt("SPUD_DATE");
                String fieldName = rs.getString("FIELD_NAME");
                String imageName = rs.getString("IMAGE_NAME");
                String imageUrl = rs.getString("IMAGE_URL");
                int phraseCount = rs.getInt("PHRASE_COUNT");
                int phraseDepth = rs.getInt("ESTIMATED_DEPTH");
                int phraseScore = rs.getInt("SCORE");
                String alias = rs.getString("ALIAS");
                String phrase = rs.getString("OCR_PHRASE");
                double lat = rs.getDouble("LATITUDE");
                double lng = rs.getDouble("LONGITUDE");

                FilteredWell filteredWell = uwiAndWells.get(uwi);
                String decodedUrl = safeDecode(imageUrl);
                if (filteredWell == null) {
                    filteredWell = FilteredWell.builder().api(uwi)
                            .name(wellName)
                            .imageUrls(new ArrayList<>())
                            .operator(operatorName)
                            .place(Place.builder()
                                    .lat(lat)
                                    .lng(lng)
                                    .location(stateName + ", " + countryName)
                                    .build())
                            .vintage(vintage)
                            .field(fieldName)
                            .phrase(WellPhrases.builder().phrases(new ArrayList<>()).build())
                            .phraseScore(phraseScore)
                            .build();
                    filteredWell.getImageUrls().add(decodedUrl);
                    uwiAndWells.put(uwi, filteredWell);
                } else {
                    if (!filteredWell.getImageUrls().contains(decodedUrl)) {
                        filteredWell.getImageUrls().add(decodedUrl);
                    }
                }
                if (phrase != null) {
                    filteredWell.getPhrase().addPhrase(WellPhrase.builder()
                            .imageName(imageName)
                            .value(safeDecode(phrase))
                            .alias(alias)
                            .count(phraseCount)
                            .depth(new ArrayList<>()).build(), phraseDepth);
                }

            }
            List<FilteredWell> wells = new ArrayList<>(uwiAndWells.values());
            wells.sort(Comparator.comparing(FilteredWell::getName));
            log.info("total rows:" + count);
            return wells;
        }
    }

    /**
     * Mapper for mapping resultset rows to a RawPhraseData
     */
    class OcrPhraseResultSetExtractor implements ResultSetExtractor<List<RawPhraseData>> {

        /**
         * map result into a RawPhraseData
         *
         * @param rs the resultset
         * @return mapped RawPhraseData
         * @throws SQLException        in case of any SQL error
         * @throws DataAccessException in case of any access error
         */
        @Override
        public List<RawPhraseData> extractData(ResultSet rs) throws SQLException, DataAccessException {
            List<RawPhraseData> phraseDatas = new ArrayList<>();
            while (rs.next()) {
                String imageUrl = rs.getString("IMAGE_URL");
                String imageName = rs.getString("IMAGE_NAME");
                String phraseType = rs.getString("OCR_PHRASE_TYPE");
                String phrase = rs.getString("OCR_PHRASE");
                int score = rs.getInt("SCORE");
                int x1 = rs.getInt("X1");
                int y1 = rs.getInt("Y1");
                int x2 = rs.getInt("X2");
                int y2 = rs.getInt("Y2");
                int phraseDepth = rs.getInt("ESTIMATED_DEPTH");
                String phaseKey = rs.getString("PHASE_KEY");
                String refKey = rs.getString("REF_KEY");
                RawPhraseData phraseData = RawPhraseData.builder().imageName(imageName)
                        .imageUrl(safeDecode(imageUrl))
                        .phraseType(phraseType)
                        .phaseKey(phaseKey)
                        .phrase(safeDecode(phrase))
                        .score(score)
                        .x1(x1)
                        .y1(y1)
                        .x2(x2)
                        .y2(y2)
                        .phraseDepth(phraseDepth)
                        .phaseKey(phaseKey)
                        .refKey(refKey).build();
                phraseDatas.add(phraseData);
            }
            return phraseDatas;
        }
    }
}
