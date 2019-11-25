package com.topcoder.quartzenergy.mudlog.search.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.Objects;

/**
 * Phrase info for a well
 */
@Data
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class WellInfoPhrase {
    /**
     * the alias
     */
    private String alias;

    /**
     * the phrase
     */
    private String phrase;

    /**
     * the height
     */
    private int height;

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
        WellInfoPhrase that = (WellInfoPhrase) o;
        return height == that.height &&
                Objects.equals(alias, that.alias) &&
                Objects.equals(phrase, that.phrase);
    }

    /**
     * overridden hash method
     *
     * @return calculated hash code of the instance
     */
    @Override
    public int hashCode() {
        return Objects.hash(alias, phrase, height);
    }
}
