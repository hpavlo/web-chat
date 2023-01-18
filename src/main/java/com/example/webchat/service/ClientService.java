package com.example.webchat.service;

import java.io.IOException;
import org.springframework.web.socket.TextMessage;
import org.springframework.web.socket.WebSocketSession;

public interface ClientService {
    void sendMessages(TextMessage message) throws IOException;

    void addClient(WebSocketSession session);

    void removeClient(WebSocketSession session);
}
