﻿using System;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;

namespace FractalPainting.App.Actions
{
    public class DragonFractalAction : IUiAction
    {
        IDragonPainterFactory dragonPainterFactory;
        Func<Random, DragonSettingsGenerator> createDragonSettingsGenerator;

        public DragonFractalAction(IDragonPainterFactory dragonPainterFactory,
            Func<Random, DragonSettingsGenerator> createDragonSettingsGenerator) 
        {
            this.dragonPainterFactory = dragonPainterFactory;
            this.createDragonSettingsGenerator = createDragonSettingsGenerator;
        }

        public string Category => "Фракталы";
        public string Name => "Дракон";
        public string Description => "Дракон Хартера-Хейтуэя";

        public void Perform()
        {
            var dragonSettings = CreateRandomSettings();
            // редактируем настройки:
            SettingsForm.For(dragonSettings).ShowDialog();
            // создаём painter с такими настройками
            dragonPainterFactory.CreateDragonPainter(dragonSettings).Paint();
        }

        private DragonSettings CreateRandomSettings()
        {
            return this.createDragonSettingsGenerator(new Random()).Generate();
        }
    }
}