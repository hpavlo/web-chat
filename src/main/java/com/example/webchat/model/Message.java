package com.example.webchat.model;

import java.time.LocalDateTime;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
public class Message {
    private String username;
    private LocalDateTime time;
    private String text;
    private MessageType messageType;
}
