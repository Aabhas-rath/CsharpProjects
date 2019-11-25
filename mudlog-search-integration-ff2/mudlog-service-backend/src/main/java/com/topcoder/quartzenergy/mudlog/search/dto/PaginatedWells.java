package com.topcoder.quartzenergy.mudlog.search.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

/**
 * class for holding paginated result
 */
import java.util.List;

@Data
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class PaginatedWells {
    /**
     * total count of the filtered result
     */
    private long total;

    /**
     * records in current page
     */
    private List<FilteredWell> records;
}
