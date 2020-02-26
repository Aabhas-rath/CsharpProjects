package com.codewithmosh.command.implementation.commands;

import com.codewithmosh.command.VideoEditor;
import com.codewithmosh.command.implementation.Command;
import com.codewithmosh.command.implementation.History;
import com.codewithmosh.command.implementation.UndoableCommand;

public class ChangeContrast implements UndoableCommand {
    private float previousValue;
    private float newValue;
    private VideoEditor videoEditor;
    private History<Command> commandHistory;

    public ChangeContrast(float newValue, VideoEditor videoEditor, History<Command> commandHistory) {
        this.newValue = newValue;
        this.videoEditor = videoEditor;
        this.commandHistory = commandHistory;
    }


    @Override
    public void unExecute() {
        videoEditor.setContrast(previousValue);
    }

    @Override
    public void execute() {
        previousValue = videoEditor.getContrast();
        videoEditor.setContrast(newValue);
        commandHistory.push(this);
    }
}
