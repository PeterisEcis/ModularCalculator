# ModularCalculator
Task: Create modular calculator where you can add functionality without rebuilding

## Overview
I created a calculator in WPF using MVVM architecture. To add functionality without rebuilding I used dll file that contained functions for calculator app. I didn't manage to make it so that you can change dll files while the program is running (so the "Reload" button is currently useless), but if I change the dll file while program is not running, then I theoretically still can add functionality without rebuilding the main app.
This can be demonstrated by taking one of the compiled dll files from [Calculator Functions](https://github.com/PeterisEcis/CalculatorFunctions) branch, for example, NoFunctions and adding it to the folder where compiled ModularCalculator.exe file will be. This will make calculator return errors when trying to do any mathematical operation. After closing the app we can then change the dll file to, for example, file from branch AdvancedDivide and after that, the program will run normally.

## Future improvements
I tried to plan ahead what would I do to improve this app. 
 - I would probably try and add some more buttons dynamically, by checking if specific methods are added to the dll file (for example, cos, sin and tan functions, square root, etc.). I checked some resources and it seems that I could use [ItemTemplate](https://social.msdn.microsoft.com/Forums/vstudio/en-US/98b2572c-befc-4b5b-88ea-56998e30f9f0/dynamic-buttons-from-data-binding-in-xaml?forum=wpf), as well as [check if dll contains a function](https://stackoverflow.com/questions/14479074/c-sharp-reflection-load-assembly-and-invoke-a-method-if-it-exists).
 - I could also work a bit on WPF magic, by adding rounded corners to buttons and some efects when mouse is over a button, just to make it prettier

## Hour 1
In the first hour I did some research since I've never worked on anything like this project. I figured out that I would use dll files to provide the functions for calculator app.
Some materials I used are: 
 - [Creating and Using DLL (Class Library) in C#](https://www.c-sharpcorner.com/UploadFile/1e050f/creating-and-using-dll-class-library-in-C-Sharp/)
 - [How to Make & Use (.dll) files in Visual Studio | Using Class Library](https://www.youtube.com/watch?v=MPOuci-6amQ&ab_channel=TACV-TheAmazingCode-Verse)

I also created 2 git repositories and projects in Visual Studio and pushed them to GitHub. The main project is [Modular Calculator](https://github.com/PeterisEcis/ModularCalculator) and the functions for the calculator will be here [Calculator Functions](https://github.com/PeterisEcis/CalculatorFunctions). 

## Hour 2
I created 3 branches in Calculator Functions project where there are different compiled dll files. I will use these files to test adding functionality without rebuilding. Also I finished rough version of the GUI for Modular Calculator, written in xaml for WPF.
After testing I found that I need to find a way to dynamically load dll files, because otherwise even after replacing the dll file it still doesn't change functionality without rebuilding.

## Hour 3
During this time I added MVVM (Model, View, ViewModel) structure with ViewModel file to handle binding data to GUI. I also created CalculationModel class that handles the mathematical functions using previously created CalculatorFunctions dll file. While looking for some info I found this old [wpf-mvvm-calculator](https://github.com/Robert-McGinley/wpf-mvvm-calculator) repository from where I took some parts of code because I thought it would be foolish not to use resources at my disposal. I also started to look on how to dynamically load dll files and I found this resource that might be useful:
 - [Loading .NET Assemblies out of Seperate Folders](https://weblog.west-wind.com/posts/2016/dec/12/loading-net-assemblies-out-of-seperate-folders)

## Hour 4
Hour 4 was not the most productive or atleast I don't have a lot to show for it. I worked on implementing new class, AssemblyManager that could dynamically load the dll file and read its methods. Some stackoverflow posts that I used:
- [C# reflection - load assembly and invoke a method if it exists](https://stackoverflow.com/questions/14479074/c-sharp-reflection-load-assembly-and-invoke-a-method-if-it-exists)
- [Activator and static classes](https://stackoverflow.com/questions/614863/activator-and-static-classes/28611155#28611155)
- [MethodBase.Invoke Method (MS Docs)](https://docs.microsoft.com/en-us/dotnet/api/system.reflection.methodbase.invoke?redirectedfrom=MSDN&view=net-6.0#overloads)

I am not sure if I will not delete it so I will just copy a short code fragment here:
``` C#
 public class AssemblyManager
    {
        private Assembly assembly;
        private Type type;

        public AssemblyManager()
        {
            assembly = Assembly.LoadFile(Constants.AssemblyFileName); // Constants.AssemblyFileName = "CalculatorFunctions"
            type = assembly.GetType(Constants.ClassName); //Constants.ClassName = "CalculatorFunctions.Functions"
        }
        
        public void ReloadAssembly()
        {
            assembly = Assembly.LoadFile(Constants.AssemblyFileName);
            type = assembly.GetType(Constants.ClassName);

        }

        public double? Add(double num1, double num2)
        {
            var info = type.GetMethod(Constants.AddMethod);
            object[] parameters = { num1, num2 };
            var result = info.Invoke(null, parameters).ToString();
            if(double.TryParse(result, out double resultDouble))
            {
                return resultDouble;
            }
            else
            {
                return null;
            }
        }
    }
```
Right now the program won't start so I'm guessing it could be the file path for assembly. 

## Hour 5
So I got the AssemblyManager to work by using Assembly.Load method instead of Assembly.LoadFile. However, right now I can't replace CalculatorMethods.dll file while the calculator is running. I can replace it after the program is stopped and technically that could work since I can just restart the .exe file without rebuilding, but I will try to make it so that I can reload it while the program is running.
Before I could get to it, I found some bugs to fix so that calculator can properly convert strings to double with InvariantCulture.

## Hour 6
I didn't manage to change dll files while the program is running. I spent the rest of my time cleaning up the code and writing overview as well as what I would do in the future if I had more time. I didn't use stopwatch to time 6 hours perfectly and in the middle I took a lunch break but I spent around 6 hours for this task as was necessary.
