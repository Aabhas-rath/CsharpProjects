package com.codewithmosh.memento.Editor;

import com.codewithmosh.memento.Document;

public class Editor {
    private Document currentDocument;


    public Document getCurrentDocument() {
        return currentDocument;
    }

    public void setCurrentDocument(Document currentDocument) {
        this.currentDocument = currentDocument;
    }

    public EditorState CreateNewState(){
        return new EditorState(currentDocument);
    }
    public void restore(EditorState state){
        currentDocument = state.getState();
    }
}
