package com.example.webchat.model;

import com.google.gson.annotations.SerializedName;

public enum MessageType {
    @SerializedName("User") USER,
    @SerializedName("CreateSession") CREATE_SESSION,
    @SerializedName("CloseSession") CLOSE_SESSION
}
