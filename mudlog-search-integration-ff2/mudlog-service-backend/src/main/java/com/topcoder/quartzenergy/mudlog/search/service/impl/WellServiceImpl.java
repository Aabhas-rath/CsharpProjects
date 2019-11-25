package com.topcoder.quartzenergy.mudlog.search.service.impl;

import com.topcoder.quartzenergy.mudlog.search.dto.FilteredWell;
import com.topcoder.quartzenergy.mudlog.search.dto.PaginatedWells;
import com.topcoder.quartzenergy.mudlog.search.dto.WellInfo;
import com.topcoder.quartzenergy.mudlog.search.repository.WellRepository;
import com.topcoder.quartzenergy.mudlog.search.service.WellService;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.net.URLDecoder;
import java.util.ArrayList;
import java.util.List;

/**
 * Implementation of {@link WellService} class
 */
@Service
@Slf4j
public class WellServiceImpl implements WellService {

    /**
     * The well repository instance
     */
    @Autowired
    private WellRepository wellRepository;

    /**
     * get all wells bounded by the extent of given lat-long
     *
     * @param minLat  latitude of top left corner of a rectangle
     * @param maxLat  latitude of bottom right corner of a rectangle
     * @param minLong longitude of top left corner of a rectangle
     * @param maxLong longitude of top bottom right of a rectangle
     * @return list of found Wells
     */
    @Override
    public List<WellInfo> getAllWells(Double minLat, Double maxLat, Double minLong, Double maxLong) {
        return wellRepository.getAllWells(minLat, maxLat, minLong, maxLong);
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
    @Override
    public PaginatedWells searchWells(List<String> uwis, List<String> states, String basin, String county,
                                      String field, int minDepth, int maxDepth, int minVintage, int maxVintage,
                                      List<String> phrases, int minPhraseCount, int maxPhraseCount,
                                      int minPhraseScore, int maxPhraseScore, Double minLat, Double maxLat,
                                      Double minLong, Double maxLong, int offset, int limit) {

        long wellCount = wellRepository.searchWellCount(uwis, states, basin, county, field, minDepth,
                maxDepth, minVintage, maxVintage, phrases, minPhraseCount, maxPhraseCount,
                minPhraseScore, maxPhraseScore, minLat, maxLat, minLong, maxLong);

        List<FilteredWell> wellList = wellRepository.searchWells(uwis, states, basin, county, field, minDepth,
                maxDepth, minVintage, maxVintage, phrases, minPhraseCount, maxPhraseCount,
                minPhraseScore, maxPhraseScore, minLat, maxLat, minLong, maxLong, offset, limit);

        postProcessPhrases(wellList);

        return PaginatedWells.builder().total(wellCount).records(wellList).build();
    }

    /**
     * Post process the phrases in well list.
     * @param wellList the well list
     */
    private void postProcessPhrases(List<FilteredWell> wellList) {
        wellList.forEach(well -> {
            well.getPhrase().getPhrases().forEach(phrase -> {
                try {
                        phrase.setValue(URLDecoder.decode( phrase.getValue(), "UTF-8" ));
                } catch (Exception e) {
                    log.warn("couldn't url decode " + phrase.getValue());
                }

                List<Integer> minMaxDepth = new ArrayList<>();
                List<Integer> depth = phrase.getDepth();

                int min = depth.stream()
                        .mapToInt(v -> v)
                        .min().orElse(0);

                int max = depth.stream()
                        .mapToInt(v -> v)
                        .max().orElse(0);

                minMaxDepth.add(min);
                minMaxDepth.add(max);
                phrase.setDepth(minMaxDepth);
            });
        });
    }
}
