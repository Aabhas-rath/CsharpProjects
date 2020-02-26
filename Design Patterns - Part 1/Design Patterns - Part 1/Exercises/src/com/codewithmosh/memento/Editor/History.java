package com.codewithmosh.memento.Editor;

import java.util.List;
import java.util.Stack;

public class History {
    private Stack<EditorState> EditorStates;
    private Stack<EditorState> EditorStatesRev;
    public History(){
        EditorStates = new Stack<>();
        EditorStatesRev = new Stack<>();
    }
    public void Push (EditorState state){
        EditorStates.push(state);
    }
    public EditorState Pop() throws Exception {
        if (!EditorStates.empty()) {
            var popped = EditorStates.pop();
            EditorStatesRev.push(popped);
            return popped;
        }
        else
            throw new Exception("Empty");
    }

    public Editor undo(Editor editor){
        var currentState = EditorStates.pop();
        editor.restore(EditorStates.pop());
        EditorStatesRev.push(currentState);
        var n = editor.getCurrentDocument().toString();
        return editor;
    }
    public Editor redo(Editor editor){
        var currentState = EditorStatesRev.pop();
        editor.restore(EditorStatesRev.pop());
        EditorStates.push(currentState);
        return editor;
    }
}
