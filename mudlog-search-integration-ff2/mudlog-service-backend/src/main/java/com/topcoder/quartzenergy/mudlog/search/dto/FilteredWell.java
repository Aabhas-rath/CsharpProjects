package com.topcoder.quartzenergy.mudlog.search.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;

/**
 * Class representing a well
 */
@Builder
@Data
@NoArgsConstructor
@AllArgsConstructor
public class FilteredWell {

    /**
     * the api of the well
     */
    private String api;

    /**
     * the name of the well
     */
    private String name;

    /**
     * Image urls for the well
     */
    private List<String> imageUrls;

    /**
     * place of the well
     */
    private Place place;

    /**
     * county name of the well
     */
    private String countyName;

    /**
     * field of the well
     */
    private String field;

    /**
     * basin name of the well
     */
    private String basinName;

    /**
     * operator name of the well
     */
    private String operator;

    /**
     * vintage year of the well
     */
    private int vintage;

    /**
     * total phrase count of the well
     */
    private int totalPhraseCount;

    /**
     * phrases associated with the well
     */
    private WellPhrases phrase;

    /**
     * phrase score of the well
     */
    private int phraseScore;
}
