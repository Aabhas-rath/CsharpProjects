package com.topcoder.quartzenergy.mudlog.search.exception;

/**
 * Mud log exception, signifying Internal server error 500
 */
public class MudlogException extends Exception {

    /**
     * Constructor with message and throwable
     *
     * @param message the message
     * @param cause   the cause
     */
    public MudlogException(String message, Throwable cause) {
        super(message, cause);
    }
}
