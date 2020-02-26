package com.codewithmosh.command.implementation.commands;

import com.codewithmosh.command.VideoEditor;
import com.codewithmosh.command.implementation.Command;
import com.codewithmosh.command.implementation.History;
import com.codewithmosh.command.implementation.UndoableCommand;

public class SetText implements UndoableCommand
{
    private String previousValue;
    private String newValue;
    private VideoEditor videoEditor;
    private History<Command> commandHistory;

    public SetText(String newValue, VideoEditor videoEditor, History<Command> commandHistory) {
        this.newValue = newValue;
        this.videoEditor = videoEditor;
        this.commandHistory = commandHistory;
    }

    @Override
    public void unExecute() {
        if (previousValue.isEmpty())
            videoEditor.removeText();
        videoEditor.setText(previousValue);
    }

    @Override
    public void execute() {
        previousValue = videoEditor.getText();
        videoEditor.setText(newValue);
        commandHistory.push(this);
    }
}
