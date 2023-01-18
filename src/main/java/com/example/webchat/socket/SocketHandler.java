package com.example.webchat.socket;

import com.example.webchat.model.Message;
import com.example.webchat.service.ClientService;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;
import com.google.gson.JsonDeserializer;
import com.google.gson.JsonSerializer;
import java.io.IOException;
import java.time.LocalDateTime;
import org.springframework.stereotype.Component;
import org.springframework.web.socket.CloseStatus;
import org.springframework.web.socket.TextMessage;
import org.springframework.web.socket.WebSocketSession;
import org.springframework.web.socket.handler.TextWebSocketHandler;

@Component
public class SocketHandler extends TextWebSocketHandler {
    private final ClientService clientService;
    private final JsonSerializer<LocalDateTime> localDateTimeJsonSerializer;
    private final JsonDeserializer<LocalDateTime> localDateTimeJsonDeserializer;
    private final Gson jsonConverter;

    public SocketHandler(ClientService clientService,
                         JsonSerializer<LocalDateTime> localDateTimeJsonSerializer,
                         JsonDeserializer<LocalDateTime> localDateTimeJsonDeserializer) {
        this.clientService = clientService;
        this.localDateTimeJsonSerializer = localDateTimeJsonSerializer;
        this.localDateTimeJsonDeserializer = localDateTimeJsonDeserializer;
        jsonConverter = createJsonConverter();
    }

    @Override
    public void afterConnectionEstablished(WebSocketSession session) {
        clientService.addClient(session);
    }

    @Override
    protected void handleTextMessage(WebSocketSession session, TextMessage textMessage)
            throws IOException {
        Message message = jsonConverter.fromJson(textMessage.getPayload(), Message.class);
        message.setTime(LocalDateTime.now());
        clientService.sendMessages(new TextMessage(jsonConverter.toJson(message)));
    }

    @Override
    public void afterConnectionClosed(WebSocketSession session, CloseStatus status) {
        clientService.removeClient(session);
    }

    private Gson createJsonConverter() {
        return new GsonBuilder()
                .registerTypeAdapter(LocalDateTime.class, localDateTimeJsonSerializer)
                .registerTypeAdapter(LocalDateTime.class, localDateTimeJsonDeserializer)
                .create();
    }
}
