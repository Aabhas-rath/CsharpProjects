package com.codewithmosh.command.implementation;

import com.codewithmosh.command.VideoEditor;

public class VideoCommandHistory extends History<UndoableCommand> {

    private VideoEditor videoEditor;

    public VideoCommandHistory(VideoEditor videoEditor) {
        this.videoEditor = videoEditor;
    }


    @Override
    public UndoableCommand undo(UndoableCommand object) {
        return null;
    }

    @Override
    public UndoableCommand redo(UndoableCommand object) {
        return null;
    }
}
