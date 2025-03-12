﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RoboTasker.Application.Common.Models;
using RoboTasker.Application.Robots.Tasks.CreateTask;

namespace RoboTasker.Api.Models.Tasks;

public class UpdateTaskRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Priority { get; set; }
    public double? Complexity { get; set; }
    public TimeSpan? EstimatedDuration { get; set; }
    
    [FromForm(Name = "properties")]
    public string? PropertiesJson { get; set; }
    
    [FromForm(Name = "requirements")]
    public string? RequirementsJson { get; set; } 
    
    [FromForm(Name = "data")]
    public string? DataJson { get; set; } 
    
    [System.Text.Json.Serialization.JsonIgnore]
    public IList<CreateTaskPropertyCommand>? Properties => JsonConvert.DeserializeObject<IList<CreateTaskPropertyCommand>>(PropertiesJson ?? "[]");
    
    [System.Text.Json.Serialization.JsonIgnore]
    public IList<CreateTaskRequirementCommand>? Requirements => JsonConvert.DeserializeObject<IList<CreateTaskRequirementCommand>>(RequirementsJson?? "[]");
    
    [System.Text.Json.Serialization.JsonIgnore]
    public IList<CreateTaskDataRequest>? Data => JsonConvert.DeserializeObject<IList<CreateTaskDataRequest>>(DataJson?? "[]");
    
    [FromForm(Name = "files")]
    public IFormFileCollection? Files { get; set; }
    
    [FromForm(Name = "deletedProperties")]
    public string? DeletedPropertiesJson { get; set; }
    
    [FromForm(Name = "deletedRequirements")]
    public string? DeletedRequirementsJson { get; set; }
    
    [FromForm(Name = "deletedData")]
    public string? DeletedDataJson { get; set; }
    
    [FromForm(Name = "deletedFiles")]
    public string? DeletedFilesJson { get; set; }
    
    [JsonIgnore]
    public IdList? DeletedProperties => ParseGuids(DeletedPropertiesJson);
    
    [JsonIgnore]
    public IdList? DeletedRequirements => ParseGuids(DeletedRequirementsJson);
    
    [JsonIgnore]
    public IdList? DeletedData => ParseGuids(DeletedDataJson);
    
    [JsonIgnore]
    public NameList? DeletedFiles => ParseFiles(DeletedFilesJson);
    
    private static NameList ParseFiles(string? json)
    {
        var list = NameList.Empty;
        if (string.IsNullOrWhiteSpace(json)) return list;
        
        try
        {
            var nameList = JsonConvert.DeserializeObject<NameList>(json);
            return nameList ?? list;
        }
        catch (ArgumentException e)
        {
            Console.WriteLine(e);
            throw;
        }
        catch (Exception e)
        {
            return list;
        }
    }
    
    private static IdList ParseGuids(string? json)
    {
        var list = IdList.Empty;
        if (string.IsNullOrWhiteSpace(json)) return list;
        
        try
        {
            var rawList = JsonConvert.DeserializeObject<IdList>(json);
            return rawList ?? list;
        }
        catch (JsonException e)
        {
            var singleId = Guid.Parse(json);
            list.Ids.Add(singleId);
            return list;
        }
        catch (Exception e)
        {
            return list;
        }
    }
}