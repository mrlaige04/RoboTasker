﻿using RoboTasker.Domain.Abstractions;

namespace RoboTasker.Domain.Chatting;

public class Chat : TenantEntity
{
    public string Name { get; set; } = null!;
    public IList<ChatUser> Users { get; set; } = [];
    public IList<ChatMessage> Messages { get; set; } = [];
}