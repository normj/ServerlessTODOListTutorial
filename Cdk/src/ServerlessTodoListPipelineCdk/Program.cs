using Amazon.CDK;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ServerlessTodoListPipelineCdk
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new App(null);
            new ServerlessTodoListPipelineCdkStack(app, "ServerlessTodoListPipelineCdkStack", new StackProps());
            app.Synth();
        }
    }
}
