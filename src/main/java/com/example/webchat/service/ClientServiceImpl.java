package com.example.webchat.service;

import java.io.IOException;
import java.util.List;
import java.util.concurrent.CopyOnWriteArrayList;
import org.springframework.stereotype.Service;
import org.springframework.web.socket.TextMessage;
import org.springframework.web.socket.WebSocketSession;

@Service
public class ClientServiceImpl implements ClientService {
    private static final List<WebSocketSession> SESSIONS = new CopyOnWriteArrayList<>();

    @Override
    public void sendMessages(TextMessage message) throws IOException {
        for (WebSocketSession session : SESSIONS) {
            session.sendMessage(message);
        }
    }

    @Override
    public void addClient(WebSocketSession session) {
        SESSIONS.add(session);
    }

    @Override
    public void removeClient(WebSocketSession session) {
        SESSIONS.remove(session);
    }
}
