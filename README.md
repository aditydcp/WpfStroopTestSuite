# WpfStroopTestSuite

WPF Application for a Test Administration App.

Developed by [Aditya Cahyo](https://github.com/aditydcp).

This repo is an alternate version of [TestSuiteWpf](https://github.com/aditydcp/TestSuiteWpf) WPF Desktop Application.
This version is using Stroop Word-Color Test.

# Installation

- Open this Solution <code>.sln</code> file in Visual Studio (or other IDE)
- Build the Solution
- Navigate to the project directory, find the output directory which should be in folder <code>/bin/</code>
- To run the app, execute the <code>.exe</code> file.

# How to Use

Before running the app, please set some configuration parameters inside <code>App.config</code> or <code>WpfStroopTestSuite.dll.config</code> file.
This file contains parameters used in the test so that user can easily manage it to suit their needs.

**Important!** Please set the value of <code>RunDataPath</code> parameter to a path to where you want the experiment data to be exported to.
Make sure to enter the path with double backslash (<code>\\\\</code>) for each level and put another one at the end.

Example: <code>C:\\\\Users\\\\Me\\\\Desktop\\\\</code>

You can also set other parameters as needed.

With that set, run the app.

## Future Development

This app is purpose-built for [Stroop Word-Color Test](https://www.sciencedirect.com/science/article/abs/pii/S2214785319313057) and is completed.

However, you can use the structure of this app to develop a test of your own.

Feel free to [contact me](https://github.com/aditydcp) if you have any question about this project or want to further development this app.
