package com.topcoder.quartzenergy.mudlog.search.service;

import com.topcoder.quartzenergy.mudlog.search.dto.PaginatedWells;
import com.topcoder.quartzenergy.mudlog.search.dto.WellInfo;

import java.util.List;

public interface WellService {

    /**
     * get all wells bounded by the extent of given lat-long
     *
     * @param minLat  latitude of top left corner of a rectangle
     * @param maxLat  latitude of bottom right corner of a rectangle
     * @param minLong longitude of top left corner of a rectangle
     * @param maxLong longitude of top bottom right of a rectangle
     * @return list of found Wells
     */
    List<WellInfo> getAllWells(Double minLat, Double maxLat, Double minLong, Double maxLong);

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
    PaginatedWells searchWells(List<String> uwis, List<String> states, String basin, String county, String field,
                               int minDepth, int maxDepth, int minVintage, int maxVintage,
                               List<String> phrases, int minPhraseCount, int maxPhraseCount,
                               int minPhraseScore, int maxPhraseScore, Double minLat, Double maxLat, Double minLong,
                               Double maxLong, int offset, int limit);
}
