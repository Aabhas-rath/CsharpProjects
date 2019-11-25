package com.topcoder.quartzenergy.mudlog.search.dto;

import lombok.*;

import java.util.List;

/**
 * Well info (for displaying on filter page)
 */
@Builder
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class WellInfo {
    /**
     * the name
     */
    private String name;

    /**
     * the api
     */
    private String api;

    /**
     * the place
     */
    private WellPlace place;

    /**
     * the operator
     */
    private String operator;

    /**
     * The vintage year
     */
    private int vintage;

    /**
     * the basin
     */
    private String basin;

    /**
     * the depth
     */
    private int depth;

    /**
     * the phrase score
     */
    private int phraseScore;

    /**
     * the image url
     */
    private List<String> imageUrls;
}
