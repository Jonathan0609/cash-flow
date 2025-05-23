﻿using AutoMapper;
using CashFlow.Application.Configurations.AutoMapper;

namespace CommonTestUtilities.Mapper;

public class MapperBuilder
{
    public static IMapper Build()
    {
        var mapper = new MapperConfiguration(config =>
        {
            config.AddProfile(new AutoMapping());
        });
        
        return mapper.CreateMapper();
    }
}