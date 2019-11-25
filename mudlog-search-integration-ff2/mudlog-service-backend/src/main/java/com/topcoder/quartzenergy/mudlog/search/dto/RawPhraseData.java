package com.topcoder.quartzenergy.mudlog.search.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

/**
 * Raw phrase data to be used in exported excel
 */
@Builder
@Data
@NoArgsConstructor
@AllArgsConstructor
public class RawPhraseData {
    /**
     * The image url (comma separated if multiple found)
     */
    String imageUrl;

    /**
     * the image name
     */
    String imageName;

    /**
     * the phrase type
     */
    String phraseType;

    /**
     * the phrase
     */
    String phrase;

    /**
     * the score
     */
    int score;

    /**
     * the value of X1
     */
    int x1;

    /**
     * the value Y1
     */
    int y1;

    /**
     * the value X2
     */
    int x2;

    /**
     * the value Y2
     */
    int y2;

    /**
     * the phrase depth
     */
    int phraseDepth;

    /**
     * the phrase key
     */
    String phaseKey;

    /**
     * the reference key
     */
    String refKey;
}
