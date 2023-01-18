package com.example.webchat.config;

import org.springframework.context.annotation.Configuration;
import org.springframework.web.socket.config.annotation.EnableWebSocket;
import org.springframework.web.socket.config.annotation.WebSocketConfigurer;
import org.springframework.web.socket.config.annotation.WebSocketHandlerRegistry;
import org.springframework.web.socket.handler.TextWebSocketHandler;

@Configuration
@EnableWebSocket
public class WebSocketConfig implements WebSocketConfigurer {
    private static final String WEB_SOCKET_PATH = "/chat";
    private final TextWebSocketHandler textWebSocketHandler;

    public WebSocketConfig(TextWebSocketHandler textWebSocketHandler) {
        this.textWebSocketHandler = textWebSocketHandler;
    }

    @Override
    public void registerWebSocketHandlers(WebSocketHandlerRegistry registry) {
        registry.addHandler(textWebSocketHandler, WEB_SOCKET_PATH);
    }
}
