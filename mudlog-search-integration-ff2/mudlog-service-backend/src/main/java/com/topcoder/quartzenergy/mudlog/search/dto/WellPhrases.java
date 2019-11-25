package com.topcoder.quartzenergy.mudlog.search.dto;

import lombok.AllArgsConstructor;
import lombok.Builder;
import lombok.Data;
import lombok.NoArgsConstructor;

import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

/**
 * Class for holding collection of phrases belonging to a well
 */
@Builder
@Data
@NoArgsConstructor
@AllArgsConstructor
public class WellPhrases {
    /**
     * list of phrases
     */
    List<WellPhrase> phrases;

    /**
     * total count
     */
    int totalCount;

    /**
     * Add a phrase to well phrases
     *
     * @param phrase the phrase to add
     * @param depth  the depth at which phrase was found
     */
    public void addPhrase(WellPhrase phrase, int depth) {
        if (phrases == null) {
            this.phrases = new ArrayList<>();
        }
        WellPhrase foundPhrase = getPhrase(phrase.getValue());
        if (foundPhrase != null) {
            foundPhrase.setCount(foundPhrase.getCount() + 1);
            addUniqueDepth(foundPhrase, depth);
        } else {
            phrase.setCount(1);
            addUniqueDepth(phrase, depth);
            phrases.add(phrase);
        }
    }

    /**
     * Add depth value if not present
     *
     * @param phrase the phrase
     * @param depth  depth found
     */
    private void addUniqueDepth(WellPhrase phrase, int depth) {
        if (!phrase.getDepth().contains(depth)) {
            phrase.getDepth().add(depth);
        }
    }

    /**
     * Get existing phrase corresponding to a phrase value
     *
     * @param phrase phrase value to check
     * @return found phrase or null otherwise
     */
    private WellPhrase getPhrase(String phrase) {
        List<WellPhrase> found = phrases.stream()
                .filter(each -> each.getValue().equalsIgnoreCase(phrase))
                .collect(Collectors.toList());
        return found.size() > 0 ? found.get(0) : null;
    }
}
