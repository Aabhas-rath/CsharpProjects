package com.topcoder.quartzenergy.mudlog.search.service.impl;

import com.topcoder.quartzenergy.mudlog.search.dto.FilterRangeBounds;
import com.topcoder.quartzenergy.mudlog.search.dto.Phrase;
import com.topcoder.quartzenergy.mudlog.search.repository.LookupRepository;
import com.topcoder.quartzenergy.mudlog.search.service.LookupService;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

/**
 * Implementation class for {@link LookupService}
 */
@Service
@Slf4j
public class LookupServiceImpl implements LookupService {

    /**
     * the lookup repository instance
     */
    @Autowired
    private LookupRepository lookupRepository;

    /**
     * get all state names
     * @return state names
     */
    @Override
    public List<String> getAllStateNames() {
        return lookupRepository.findAllStatesNames();
    }
    /**
     * get all basin names
     * @return basin names
     */
    @Override
    public List<String> getAllBasinNames() {
        return lookupRepository.findAllBasinNames();
    }

    /**
     * get all phrase types
     * @return phrase types
     */
    @Override
    public List<String> getAllPhraseTypes() {
        return lookupRepository.findAllPhraseTypes();
    }

    /**
     * get all phrases
     * @return phrases
     */
    @Override
    public List<Phrase> getAllPhrases() {
        return lookupRepository.findAllPhrases();
    }

    /**
     * get all county names
     * @return county names
     */
    @Override
    public List<String> getCountyNamesByStateName(List<String> states) {
        if (states.contains("all")) {
            return lookupRepository.findAllCountyNames();
        }
        if (states.size() == 0) {
            return new ArrayList<>();
        }
        return lookupRepository.getCountyNamesByState(states);
    }

    /**
     * get all field names
     * @return field names
     */
    @Override
    public List<String> getFieldNames(Optional<List<String>> states, Optional<String> basin) {
        if (states.isPresent()) {
            List<String> stateNames = states.get();
            if (stateNames.contains("all")) {
                return lookupRepository.findAllFieldNames();
            }
            if (stateNames.size() == 0) {
                return new ArrayList<>();
            }
            return lookupRepository.getFieldNamesByState(stateNames);
        }
        if (basin.isPresent()) {
            return lookupRepository.getFieldNamesByBasin(basin.get());
        }

        return new ArrayList<>();
    }

    /**
     * get filter bounds (max/min values)
     * @return filter bounds
     */
    @Override
    public FilterRangeBounds getFilterBounds() {
        int maxDepth = lookupRepository.findMaxDepth();
        int minVintageYear = lookupRepository.findMinVintageYear();
        int maxPhraseCount = lookupRepository.findMaxPhraseCount();
        int maxPhraseScore = lookupRepository.findMaxPhraseScore();
        return FilterRangeBounds.builder().maxDepth(maxDepth)
                .minVintage(minVintageYear)
                .maxPhraseCount(maxPhraseCount)
                .maxPhraseScore(maxPhraseScore)
                .build();
    }

    /**
     * get all phrases aliases
     * @return phrase aliases
     */
    @Override
    public List<String> getPhraseAliases() {
        return lookupRepository.getPhraseAliases();
    }
}
