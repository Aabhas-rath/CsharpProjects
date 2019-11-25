package com.topcoder.quartzenergy.mudlog.search.utils;

import java.io.UnsupportedEncodingException;
import java.net.URLDecoder;

public class MudlogUtils {

    /**
     * Decode text
     * @param text the text to decode
     * @return decode string if successful or the original text
     */
    public static String safeDecode(String text) {
        try {
            return URLDecoder.decode( text, "UTF-8" );
        } catch (UnsupportedEncodingException e) {
            return text;
        }
    }
}
