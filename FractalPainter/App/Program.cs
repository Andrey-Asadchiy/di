using System;
using System.Windows.Forms;
using FractalPainting.App.Actions;
using FractalPainting.App.Fractals;
using FractalPainting.Infrastructure.Common;
using FractalPainting.Infrastructure.UiActions;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;

namespace FractalPainting.App
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            try
            {                
                // start here             
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                var container = new StandardKernel();

                container.Bind(kernel => kernel
                    .FromThisAssembly()
                    .SelectAllClasses()
                    .InheritedFrom<IUiAction>()
                    .BindAllInterfaces());

                container.Bind<Palette>().ToSelf().InSingletonScope();
                container.Bind<IImageHolder, PictureBoxImageHolder>()
                    .To<PictureBoxImageHolder>()
                    .InSingletonScope();

                container.Bind<IDragonPainterFactory>().ToFactory();
         
                container.Bind<IObjectSerializer>()
                    .To<XmlObjectSerializer>()                    
                    .WhenInjectedInto<SettingsManager>();
                container.Bind<IBlobStorage>()
                    .To<FileBlobStorage>()
                    .WhenInjectedInto<SettingsManager>();
                container.Bind<IImageDirectoryProvider, IImageSettingsProvider>()
                    .ToMethod(c => c.Kernel.Get<SettingsManager>().Load());
                container.Bind<ImageSettings>()
                    .ToMethod(c => c.Kernel.Get<IImageSettingsProvider>().ImageSettings);

                Application.Run(container.Get<MainForm>());
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}