using AutoMapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace CANAdminApi.Services.Automapper
{
    public class AutomapperConfig : Mapper
    {
        public AutomapperConfig(IConfigurationProvider configurationProvider)
            : base(configurationProvider) { }
        public static void RegisterMappings()
        {
            var config = new MapperConfiguration(cfg =>
                cfg.AddMaps(
                Assembly.GetExecutingAssembly()
                )
            );
            config.CompileMappings();
        }

        public static Mapper Mapper
        {
            get
            {
                var config = new MapperConfiguration(cfg =>
                cfg.AddMaps(
                Assembly.GetExecutingAssembly()
                )
            );
                config.CompileMappings();

                return new Mapper(config);
            }
        }
    }
}
