package com.topcoder.quartzenergy.mudlog.search.repository;

import com.topcoder.quartzenergy.mudlog.search.dto.Phrase;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.RowMapper;
import org.springframework.jdbc.core.namedparam.MapSqlParameterSource;
import org.springframework.jdbc.core.namedparam.NamedParameterJdbcTemplate;
import org.springframework.stereotype.Repository;
import org.springframework.transaction.annotation.Transactional;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;

/**
 * Repository class for lookup queries
 */
@Repository
public class LookupRepository {

    /**
     * Query for fetching filter max depth value
     */
    public static final String FILTER_MAX_DEPTH_QUERY = "select MAX(icd.[DEPTH]) from IMAGE_OCR_DEPTH icd";

    /**
     * Query for fetching filter min vintage year value
     */
    public static final String FILTER_MIN_VINTAGE_YEAR_QUERY = "select MIN(YEAR(wl.SPUD_DATE)) from well_info wl";

    /**
     * Query for fetching filter max phrase count value
     */
    public static final String FILTER_MAX_PHRASE_COUNT_QUERY = "select MAX(iocr.PHRASE_COUNT) from IMAGE_OCR iocr";

    /**
     * Query for fetching filter max phrase score value
     */
    public static final String FILTER_MAX_PHRASE_SCORE_QUERY = "select MAX(iocr.SCORE) from IMAGE_OCR iocr";

    /**
     * Query for fetching state names
     */
    public static final String LOOKUP_STATE_NAMES_QUERY = "SELECT DISTINCT PROVINCE_STATE_NAME as name FROM well_info";

    /**
     * Query for fetching basin names
     */
    public static final String LOOKUP_BASIN_NAMES_QUERY = "SELECT DISTINCT BASIN_NAME as name FROM well_info";

    /**
     * Query for fetching county names
     */
    public static final String LOOKUP_COUNTY_NAMES_QUERY = "SELECT DISTINCT COUNTY_NAME as name FROM well_info";

    /**
     * Query for fetching phrase types names
     */
    public static final String LOOKUP_PHRASE_TYPES_QUERY = "SELECT DISTINCT OCR_PHRASE_TYPE as name " +
            "FROM IMAGE_OCR_PHRASE";

    /**
     * Query for fetching all phrases with category
     */
    public static final String LOOKUP_ALL_PHRASES_QUERY = "SELECT DISTINCT OCR_PHRASE_TYPE as category, " +
            "OCR_PHRASE as phrase FROM IMAGE_OCR_PHRASE";

    /**
     * Query for fetching field names
     */
    public static final String LOOKUP_ALL_FIELD_NAMES_QUERY = "SELECT DISTINCT FIELD_NAME as name FROM well_info";

    /**
     * Query for fetching phrase aliases
     */
    public static final String LOOKUP_ALL_PHRASE_ALIASES = "select DISTINCT iop.ALIAS from IMAGE_OCR_PHRASE iop " +
            "where iop.ALIAS != '' order by iop.ALIAS ASC";

    /**
     * Query for fetching county for a state
     */
    public static final String GET_COUNTY_BY_STATES_QUERY = "SELECT DISTINCT COUNTY_NAME as name FROM well_info " +
            "where PROVINCE_STATE_NAME in (:stateNames)";

    /**
     * Query for fetching field names for a state
     */
    public static final String GET_FIELD_NAMES_BY_STATES_QUERY = "SELECT DISTINCT FIELD_NAME as name FROM well_info " +
            "where PROVINCE_STATE_NAME in (:stateNames)";

    /**
     * Query for fetching field names for a basin names
     */
    public static final String GET_FIELD_NAMES_BY_BASIN_QUERY = "SELECT DISTINCT FIELD_NAME as name FROM well_info " +
            "where BASIN_NAME=?";

    /**
     * the JDBC template
     */
    private JdbcTemplate jdbcTemplate;

    /**
     * the NamedParams JDBC template
     */
    private NamedParameterJdbcTemplate namedJdbcTemplate;

    /**
     * Autowired Constructor
     *
     * @param jdbcTemplate the jdbc template
     */
    @Autowired
    public LookupRepository(JdbcTemplate jdbcTemplate) {
        this.jdbcTemplate = jdbcTemplate;
        this.namedJdbcTemplate = new NamedParameterJdbcTemplate(jdbcTemplate.getDataSource());
    }

    /**
     * Load all state names
     *
     * @return states
     */
    @Transactional(readOnly = true)
    public List<String> findAllStatesNames() {
        return jdbcTemplate.queryForList(LOOKUP_STATE_NAMES_QUERY, String.class);
    }

    /**
     * Load all basin names
     *
     * @return basins
     */
    @Transactional(readOnly = true)
    public List<String> findAllBasinNames() {
        return jdbcTemplate.queryForList(LOOKUP_BASIN_NAMES_QUERY, String.class);

    }

    /**
     * Load all county names
     *
     * @return counties
     */
    @Transactional(readOnly = true)
    public List<String> findAllCountyNames() {
        return jdbcTemplate.queryForList(LOOKUP_COUNTY_NAMES_QUERY, String.class);
    }

    /**
     * Load all phrase types
     *
     * @return phrase types
     */
    @Transactional(readOnly = true)
    public List<String> findAllPhraseTypes() {
        return jdbcTemplate.queryForList(LOOKUP_PHRASE_TYPES_QUERY, String.class);
    }

    /**
     * Load all phrases
     *
     * @return phrases
     */
    @Transactional(readOnly = true)
    public List<Phrase> findAllPhrases() {
        return jdbcTemplate.query(LOOKUP_ALL_PHRASES_QUERY, new PhraseMapper());
    }

    /**
     * Load all field names
     *
     * @return fields
     */
    @Transactional(readOnly = true)
    public List<String> findAllFieldNames() {
        return jdbcTemplate.queryForList(LOOKUP_ALL_FIELD_NAMES_QUERY, String.class);
    }

    /**
     * Load all county names by a state
     *
     * @return county names
     */
    @Transactional(readOnly = true)
    public List<String> getCountyNamesByState(List<String> states) {
        MapSqlParameterSource parameters = new MapSqlParameterSource();
        parameters.addValue("stateNames", states);

        return namedJdbcTemplate.queryForList(GET_COUNTY_BY_STATES_QUERY,
                parameters,
                String.class);
    }

    /**
     * Load all field names for a list of state names
     *
     * @return field names
     */
    @Transactional(readOnly = true)
    public List<String> getFieldNamesByState(List<String> states) {
        MapSqlParameterSource parameters = new MapSqlParameterSource();
        parameters.addValue("stateNames", states);
        return namedJdbcTemplate.queryForList(GET_FIELD_NAMES_BY_STATES_QUERY,
                parameters,
                String.class);
    }

    /**
     * Load all field names for a basin name
     *
     * @return field names
     */
    @Transactional(readOnly = true)
    public List<String> getFieldNamesByBasin(String basin) {
        return jdbcTemplate.queryForList(GET_FIELD_NAMES_BY_BASIN_QUERY,
                new Object[]{basin},
                String.class);
    }

    /**
     * Get max value of depth for a well
     *
     * @return max depth
     */
    @Transactional(readOnly = true)
    public int findMaxDepth() {
        return jdbcTemplate.queryForObject(FILTER_MAX_DEPTH_QUERY, Integer.class);
    }

    /**
     * Get min value of vintage year for a well
     *
     * @return min vintage
     */
    @Transactional(readOnly = true)
    public int findMinVintageYear() {
        return jdbcTemplate.queryForObject(FILTER_MIN_VINTAGE_YEAR_QUERY, Integer.class);
    }

    /**
     * Get max value of phrase count for a well
     *
     * @return max count
     */
    @Transactional(readOnly = true)
    public int findMaxPhraseCount() {
        return jdbcTemplate.queryForObject(FILTER_MAX_PHRASE_COUNT_QUERY, Integer.class);
    }

    /**
     * Get max value of phrase score for a well
     *
     * @return max count
     */
    @Transactional(readOnly = true)
    public int findMaxPhraseScore() {
        return jdbcTemplate.queryForObject(FILTER_MAX_PHRASE_SCORE_QUERY, Integer.class);
    }

    /**
     * Get all phrase aliases
     *
     * @return phrase aliases
     */
    public List<String> getPhraseAliases() {
        return jdbcTemplate.queryForList(LOOKUP_ALL_PHRASE_ALIASES, String.class);
    }

    /**
     * Row mapper for wrapping resultset into Phrase class instance
     */
    class PhraseMapper implements RowMapper<Phrase> {
        /**
         * Map resulset to Phrase
         *
         * @param rs     the resultset
         * @param rowNum the row number
         * @return instance of Phrase
         * @throws SQLException in case of any error
         */
        @Override
        public Phrase mapRow(ResultSet rs, int rowNum) throws SQLException {
            Phrase phrase = new Phrase();
            phrase.setCategory(rs.getString("category"));
            phrase.setPhrase(rs.getString("phrase"));
            return phrase;
        }
    }

}
