using System.Reflection;
using System.Runtime.InteropServices;
using AudioMixer;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("AudioMixer")]
[assembly: AssemblyDescription("Программа для одновременного воспроизведения нескольких треков на произвольных аудио-устройствах.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("xewee")]
[assembly: AssemblyProduct("AudioMixer")]
[assembly: AssemblyCopyright("xewee ©  2019")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("010de1ee-1c8d-4e51-8766-714b55405e57")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion(AssemblyInfo.VERSION)]
[assembly: AssemblyFileVersion(AssemblyInfo.VERSION)]

public static class AssemblyInfo
{
	public const string VERSION = "1.2.6.1";
}