package com.topcoder.quartzenergy.mudlog.search.controller;

import com.topcoder.quartzenergy.mudlog.search.dto.FilterRangeBounds;
import com.topcoder.quartzenergy.mudlog.search.dto.Phrase;
import com.topcoder.quartzenergy.mudlog.search.service.LookupService;
import lombok.extern.slf4j.Slf4j;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;

import java.util.List;
import java.util.Optional;

/**
 * Controller class for handling lookup data
 */
@Slf4j
@RequestMapping("lookup")
@RestController
public class LookupController {

    /**
     * the lookup service
     */
    @Autowired
    private LookupService lookupService;

    /**
     * get all state names
     * @return state names
     */
    @GetMapping("/states")
    public List<String> getAllStateNames() {
        return lookupService.getAllStateNames();
    }

    /**
     * get all basin names
     * @return basin names
     */
    @GetMapping("/basins")
    public List<String> getAllBasinNames() {
        return lookupService.getAllBasinNames();
    }

    /**
     * get all phrase types
     * @return phrase types
     */
    @GetMapping("/phrase-types")
    public List<String> getAllPhraseTypes() {
        return lookupService.getAllPhraseTypes();
    }

    /**
     * get all phrases
     * @return phrases
     */
    @GetMapping("/phrases")
    public List<Phrase> getAllPhrases() {
        return lookupService.getAllPhrases();
    }

    /**
     * get all county names
     * @return county names
     */
    @GetMapping("/counties")
    public List<String> getStateCountyNames(@RequestParam(name = "states") List<String> states) {
        return lookupService.getCountyNamesByStateName(states);
    }

    /**
     * get all field names
     * @return field names
     */
    @GetMapping("/fields")
    public List<String> getFieldNames(@RequestParam(name = "states", required = false) Optional<List<String>> states,
                                      @RequestParam(name = "basin", required = false) Optional<String> basin) {
        return lookupService.getFieldNames(states, basin);
    }

    /**
     * get filter bounds (max/min values)
     * @return filter bounds
     */
    @GetMapping("filter-range-bounds")
    public FilterRangeBounds getFilterBounds() {
        return lookupService.getFilterBounds();
    }

    /**
     * get all phrases aliases
     * @return phrase aliases
     */
    @GetMapping("phrase-aliases")
    public List<String> getPhraseAliases() {
        return lookupService.getPhraseAliases();
    }
}
