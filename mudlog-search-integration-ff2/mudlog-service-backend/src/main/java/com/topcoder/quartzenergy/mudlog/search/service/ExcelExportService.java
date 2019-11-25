package com.topcoder.quartzenergy.mudlog.search.service;


import com.topcoder.quartzenergy.mudlog.search.exception.MudlogException;

import javax.servlet.http.HttpServletResponse;
import java.util.List;

/**
 * Service class for handling well exports to excel
 */
public interface ExcelExportService {

    /**
     * Export filtered list of Well to excel
     *
     * @param response       the http response
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
     * @return created excel byte array content
     * @throws MudlogException in case of any errors found while generating excel
     */
    byte[] export(HttpServletResponse response, List<String> uwis, List<String> states, String basin,
                  String county, String field, int minDepth, int maxDepth, int minVintage, int maxVintage,
                  List<String> phrases, int minPhraseCount, int maxPhraseCount, int minPhraseScore,
                  int maxPhraseScore, Double minLat, Double maxLat, Double minLong, Double maxLong) throws MudlogException;
}
