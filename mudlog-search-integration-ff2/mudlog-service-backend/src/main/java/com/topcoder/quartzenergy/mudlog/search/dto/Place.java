package com.topcoder.quartzenergy.mudlog.search.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

/**
 * A well's place
 */
@Builder
@Data
@NoArgsConstructor
@AllArgsConstructor
public class Place {
    /**
     * the location
     */
    String location;

    /**
     * the latitude
     */
    double lat;

    /**
     * the longitude
     */
    double lng;
}
