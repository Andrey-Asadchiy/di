using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using System;

namespace FractalPainting.App.Actions
{
    public class KochFractalAction : IUiAction
    {
        Lazy<KochPainter> kochPainter;

        public KochFractalAction(Lazy<KochPainter> kochPainter) 
        {
            this.kochPainter  = kochPainter;
        }

        public string Category => "Фракталы";
        public string Name => "Кривая Коха";
        public string Description => "Кривая Коха";

        public void Perform()
        {            
            this.kochPainter.Value.Paint();
        }
    }
}