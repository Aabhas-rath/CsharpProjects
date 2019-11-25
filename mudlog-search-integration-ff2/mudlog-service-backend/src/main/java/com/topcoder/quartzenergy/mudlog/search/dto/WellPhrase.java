package com.topcoder.quartzenergy.mudlog.search.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.List;
import java.util.Objects;

/**
 * Phrase info (for showing on filtered page)
 */
@Builder
@Data
@NoArgsConstructor
@AllArgsConstructor
public class WellPhrase {
    /**
     * the image name
     */

    String imageName;
    /**
     * the alias
     */
    String alias;

    /**
     * the phrase text
     */
    String value;

    /**
     * list of depths at which phrase was found
     */
    List<Integer> depth;

    /**
     * number of time phrase appeared
     */
    int count;

    /**
     * overridden equals method
     *
     * @param o the object to check for equality
     * @return flag indicating if object was found equal
     */
    @Override
    public boolean equals(Object o) {
        if (this == o) return true;
        if (o == null || getClass() != o.getClass()) return false;
        WellPhrase that = (WellPhrase) o;
        return Objects.equals(value, that.value);
    }

    /**
     * overridden hash method
     *
     * @return calculated hash code of the instance
     */
    @Override
    public int hashCode() {
        return Objects.hash(value);
    }
}
