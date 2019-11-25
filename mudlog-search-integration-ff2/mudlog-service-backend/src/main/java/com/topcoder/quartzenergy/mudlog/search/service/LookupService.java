package com.topcoder.quartzenergy.mudlog.search.service;

import com.topcoder.quartzenergy.mudlog.search.dto.FilterRangeBounds;
import com.topcoder.quartzenergy.mudlog.search.dto.Phrase;

import java.util.List;
import java.util.Optional;

/**
 * Service class handling lookups
 */
public interface LookupService {

    /**
     * get all state names
     * @return state names
     */
    List<String> getAllStateNames();

    /**
     * get all basin names
     * @return basin names
     */
    List<String> getAllBasinNames();

    /**
     * get all phrase types
     * @return phrase types
     */
    List<String> getAllPhraseTypes();

    /**
     * get all phrases
     * @return phrases
     */
    List<Phrase> getAllPhrases();

    /**
     * get all county names
     * @return county names
     */
    List<String> getCountyNamesByStateName(List<String> state);

    /**
     * get all field names
     * @return field names
     */
    List<String> getFieldNames(Optional<List<String>> state, Optional<String> basin);

    /**
     * get filter bounds (max/min values)
     * @return filter bounds
     */
    FilterRangeBounds getFilterBounds();

    /**
     * get all phrases aliases
     * @return phrase aliases
     */
    List<String> getPhraseAliases();
}
