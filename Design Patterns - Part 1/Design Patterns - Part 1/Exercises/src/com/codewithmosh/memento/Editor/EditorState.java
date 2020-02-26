package com.codewithmosh.memento.Editor;

import com.codewithmosh.memento.Document;

public class EditorState {
    private final Document State;
    public EditorState(Document state) {
        State = state;
    }

    public Document getState() {
        return State;
    }
}
