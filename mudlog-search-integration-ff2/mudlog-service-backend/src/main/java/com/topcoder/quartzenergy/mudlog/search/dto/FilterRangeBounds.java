package com.topcoder.quartzenergy.mudlog.search.dto;

import lombok.*;

/**
 * Class for holding bounds (min/max) for range filter
 */
@Builder
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
public class FilterRangeBounds {
    /**
     * max depth in current database
     */
    private int maxDepth;

    /**
     * min vintage in current database
     */
    private int minVintage;

    /**
     * max phrase count in current database
     */
    private int maxPhraseCount;

    /**
     * max phrase score in current database
     */
    private int maxPhraseScore;
}
