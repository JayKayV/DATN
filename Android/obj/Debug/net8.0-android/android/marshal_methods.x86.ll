; ModuleID = 'marshal_methods.x86.ll'
source_filename = "marshal_methods.x86.ll"
target datalayout = "e-m:e-p:32:32-p270:32:32-p271:32:32-p272:64:64-f64:32:64-f80:32-n8:16:32-S128"
target triple = "i686-unknown-linux-android21"

%struct.MarshalMethodName = type {
	i64, ; uint64_t id
	ptr ; char* name
}

%struct.MarshalMethodsManagedClass = type {
	i32, ; uint32_t token
	ptr ; MonoClass klass
}

@assembly_image_cache = dso_local local_unnamed_addr global [215 x ptr] zeroinitializer, align 4

; Each entry maps hash of an assembly name to an index into the `assembly_image_cache` array
@assembly_image_cache_hashes = dso_local local_unnamed_addr constant [424 x i32] [
	i32 2616222, ; 0: System.Net.NetworkInformation.dll => 0x27eb9e => 68
	i32 10166715, ; 1: System.Net.NameResolution.dll => 0x9b21bb => 67
	i32 15721112, ; 2: System.Runtime.Intrinsics.dll => 0xefe298 => 108
	i32 32687329, ; 3: Xamarin.AndroidX.Lifecycle.Runtime => 0x1f2c4e1 => 193
	i32 34715100, ; 4: Xamarin.Google.Guava.ListenableFuture.dll => 0x211b5dc => 202
	i32 34839235, ; 5: System.IO.FileSystem.DriveInfo => 0x2139ac3 => 48
	i32 39109920, ; 6: Newtonsoft.Json.dll => 0x254c520 => 181
	i32 39485524, ; 7: System.Net.WebSockets.dll => 0x25a8054 => 80
	i32 42639949, ; 8: System.Threading.Thread => 0x28aa24d => 145
	i32 66541672, ; 9: System.Diagnostics.StackTrace => 0x3f75868 => 30
	i32 68219467, ; 10: System.Security.Cryptography.Primitives => 0x410f24b => 124
	i32 72070932, ; 11: Microsoft.Maui.Graphics.dll => 0x44bb714 => 176
	i32 82292897, ; 12: System.Runtime.CompilerServices.VisualC.dll => 0x4e7b0a1 => 102
	i32 117431740, ; 13: System.Runtime.InteropServices => 0x6ffddbc => 107
	i32 122350210, ; 14: System.Threading.Channels.dll => 0x74aea82 => 139
	i32 134690465, ; 15: Xamarin.Kotlin.StdLib.Jdk7.dll => 0x80736a1 => 206
	i32 142721839, ; 16: System.Net.WebHeaderCollection => 0x881c32f => 77
	i32 149972175, ; 17: System.Security.Cryptography.Primitives.dll => 0x8f064cf => 124
	i32 159306688, ; 18: System.ComponentModel.Annotations => 0x97ed3c0 => 13
	i32 165246403, ; 19: Xamarin.AndroidX.Collection.dll => 0x9d975c3 => 188
	i32 176265551, ; 20: System.ServiceProcess => 0xa81994f => 132
	i32 184328833, ; 21: System.ValueTuple.dll => 0xafca281 => 151
	i32 205061960, ; 22: System.ComponentModel => 0xc38ff48 => 18
	i32 209399409, ; 23: Xamarin.AndroidX.Browser.dll => 0xc7b2e71 => 187
	i32 220171995, ; 24: System.Diagnostics.Debug => 0xd1f8edb => 26
	i32 230752869, ; 25: Microsoft.CSharp.dll => 0xdc10265 => 1
	i32 231409092, ; 26: System.Linq.Parallel => 0xdcb05c4 => 59
	i32 231814094, ; 27: System.Globalization => 0xdd133ce => 42
	i32 246610117, ; 28: System.Reflection.Emit.Lightweight => 0xeb2f8c5 => 91
	i32 276479776, ; 29: System.Threading.Timer.dll => 0x107abf20 => 147
	i32 280482487, ; 30: Xamarin.AndroidX.Interpolator => 0x10b7d2b7 => 191
	i32 291076382, ; 31: System.IO.Pipes.AccessControl.dll => 0x1159791e => 54
	i32 298918909, ; 32: System.Net.Ping.dll => 0x11d123fd => 69
	i32 321597661, ; 33: System.Numerics => 0x132b30dd => 83
	i32 342366114, ; 34: Xamarin.AndroidX.Lifecycle.Common => 0x146817a2 => 192
	i32 346219089, ; 35: Autofac => 0x14a2e251 => 173
	i32 360082299, ; 36: System.ServiceModel.Web => 0x15766b7b => 131
	i32 367780167, ; 37: System.IO.Pipes => 0x15ebe147 => 55
	i32 374914964, ; 38: System.Transactions.Local => 0x1658bf94 => 149
	i32 375677976, ; 39: System.Net.ServicePoint.dll => 0x16646418 => 74
	i32 379916513, ; 40: System.Threading.Thread.dll => 0x16a510e1 => 145
	i32 385762202, ; 41: System.Memory.dll => 0x16fe439a => 62
	i32 392610295, ; 42: System.Threading.ThreadPool.dll => 0x1766c1f7 => 146
	i32 395744057, ; 43: _Microsoft.Android.Resource.Designer => 0x17969339 => 211
	i32 403441872, ; 44: WindowsBase => 0x180c08d0 => 165
	i32 442565967, ; 45: System.Collections => 0x1a61054f => 12
	i32 451504562, ; 46: System.Security.Cryptography.X509Certificates => 0x1ae969b2 => 125
	i32 456227837, ; 47: System.Web.HttpUtility.dll => 0x1b317bfd => 152
	i32 459347974, ; 48: System.Runtime.Serialization.Primitives.dll => 0x1b611806 => 113
	i32 465846621, ; 49: mscorlib => 0x1bc4415d => 166
	i32 469710990, ; 50: System.dll => 0x1bff388e => 164
	i32 476646585, ; 51: Xamarin.AndroidX.Interpolator.dll => 0x1c690cb9 => 191
	i32 498788369, ; 52: System.ObjectModel => 0x1dbae811 => 84
	i32 507640256, ; 53: MonoGame.Framework => 0x1e41f9c0 => 180
	i32 526420162, ; 54: System.Transactions.dll => 0x1f6088c2 => 150
	i32 527452488, ; 55: Xamarin.Kotlin.StdLib.Jdk7 => 0x1f704948 => 206
	i32 530272170, ; 56: System.Linq.Queryable => 0x1f9b4faa => 60
	i32 540030774, ; 57: System.IO.FileSystem.dll => 0x20303736 => 51
	i32 545304856, ; 58: System.Runtime.Extensions => 0x2080b118 => 103
	i32 546455878, ; 59: System.Runtime.Serialization.Xml => 0x20924146 => 114
	i32 549171840, ; 60: System.Globalization.Calendars => 0x20bbb280 => 40
	i32 557405415, ; 61: Jsr305Binding => 0x213954e7 => 199
	i32 565490802, ; 62: Android => 0x21b4b472 => 0
	i32 577335427, ; 63: System.Security.Cryptography.Cng => 0x22697083 => 120
	i32 601371474, ; 64: System.IO.IsolatedStorage.dll => 0x23d83352 => 52
	i32 605376203, ; 65: System.IO.Compression.FileSystem => 0x24154ecb => 44
	i32 613668793, ; 66: System.Security.Cryptography.Algorithms => 0x2493d7b9 => 119
	i32 643868501, ; 67: System.Net => 0x2660a755 => 81
	i32 662205335, ; 68: System.Text.Encodings.Web.dll => 0x27787397 => 136
	i32 663517072, ; 69: Xamarin.AndroidX.VersionedParcelable => 0x278c7790 => 198
	i32 666292255, ; 70: Xamarin.AndroidX.Arch.Core.Common.dll => 0x27b6d01f => 185
	i32 672442732, ; 71: System.Collections.Concurrent => 0x2814a96c => 8
	i32 683518922, ; 72: System.Net.Security => 0x28bdabca => 73
	i32 690569205, ; 73: System.Xml.Linq.dll => 0x29293ff5 => 155
	i32 691348768, ; 74: Xamarin.KotlinX.Coroutines.Android.dll => 0x29352520 => 208
	i32 693804605, ; 75: System.Windows => 0x295a9e3d => 154
	i32 699345723, ; 76: System.Reflection.Emit => 0x29af2b3b => 92
	i32 700284507, ; 77: Xamarin.Jetbrains.Annotations => 0x29bd7e5b => 203
	i32 700358131, ; 78: System.IO.Compression.ZipFile => 0x29be9df3 => 45
	i32 720511267, ; 79: Xamarin.Kotlin.StdLib.Jdk8 => 0x2af22123 => 207
	i32 722857257, ; 80: System.Runtime.Loader.dll => 0x2b15ed29 => 109
	i32 735137430, ; 81: System.Security.SecureString.dll => 0x2bd14e96 => 129
	i32 752232764, ; 82: System.Diagnostics.Contracts.dll => 0x2cd6293c => 25
	i32 759454413, ; 83: System.Net.Requests => 0x2d445acd => 72
	i32 762598435, ; 84: System.IO.Pipes.dll => 0x2d745423 => 55
	i32 775507847, ; 85: System.IO.Compression => 0x2e394f87 => 46
	i32 789583800, ; 86: SharedLibrary => 0x2f1017b8 => 210
	i32 790455559, ; 87: MonoGame.Extended.Tiled => 0x2f1d6507 => 179
	i32 804715423, ; 88: System.Data.Common => 0x2ff6fb9f => 22
	i32 823281589, ; 89: System.Private.Uri.dll => 0x311247b5 => 86
	i32 830298997, ; 90: System.IO.Compression.Brotli => 0x317d5b75 => 43
	i32 832635846, ; 91: System.Xml.XPath.dll => 0x31a103c6 => 160
	i32 834051424, ; 92: System.Net.Quic => 0x31b69d60 => 71
	i32 873119928, ; 93: Microsoft.VisualBasic => 0x340ac0b8 => 3
	i32 877678880, ; 94: System.Globalization.dll => 0x34505120 => 42
	i32 878954865, ; 95: System.Net.Http.Json => 0x3463c971 => 63
	i32 904024072, ; 96: System.ComponentModel.Primitives.dll => 0x35e25008 => 16
	i32 911108515, ; 97: System.IO.MemoryMappedFiles.dll => 0x364e69a3 => 53
	i32 928116545, ; 98: Xamarin.Google.Guava.ListenableFuture => 0x3751ef41 => 202
	i32 952186615, ; 99: System.Runtime.InteropServices.JavaScript.dll => 0x38c136f7 => 105
	i32 955402788, ; 100: Newtonsoft.Json => 0x38f24a24 => 181
	i32 956575887, ; 101: Xamarin.Kotlin.StdLib.Jdk8.dll => 0x3904308f => 207
	i32 966729478, ; 102: Xamarin.Google.Crypto.Tink.Android => 0x399f1f06 => 200
	i32 967690846, ; 103: Xamarin.AndroidX.Lifecycle.Common.dll => 0x39adca5e => 192
	i32 975236339, ; 104: System.Diagnostics.Tracing => 0x3a20ecf3 => 34
	i32 975874589, ; 105: System.Xml.XDocument => 0x3a2aaa1d => 158
	i32 986514023, ; 106: System.Private.DataContractSerialization.dll => 0x3acd0267 => 85
	i32 987214855, ; 107: System.Diagnostics.Tools => 0x3ad7b407 => 32
	i32 992768348, ; 108: System.Collections.dll => 0x3b2c715c => 12
	i32 994442037, ; 109: System.IO.FileSystem => 0x3b45fb35 => 51
	i32 1000284451, ; 110: SharedLibrary.dll => 0x3b9f2123 => 210
	i32 1001831731, ; 111: System.IO.UnmanagedMemoryStream.dll => 0x3bb6bd33 => 56
	i32 1019214401, ; 112: System.Drawing => 0x3cbffa41 => 36
	i32 1031528504, ; 113: Xamarin.Google.ErrorProne.Annotations.dll => 0x3d7be038 => 201
	i32 1036536393, ; 114: System.Drawing.Primitives.dll => 0x3dc84a49 => 35
	i32 1044663988, ; 115: System.Linq.Expressions.dll => 0x3e444eb4 => 58
	i32 1067306892, ; 116: GoogleGson => 0x3f9dcf8c => 174
	i32 1082857460, ; 117: System.ComponentModel.TypeConverter => 0x408b17f4 => 17
	i32 1084122840, ; 118: Xamarin.Kotlin.StdLib => 0x409e66d8 => 204
	i32 1098259244, ; 119: System => 0x41761b2c => 164
	i32 1170634674, ; 120: System.Web.dll => 0x45c677b2 => 153
	i32 1204270330, ; 121: Xamarin.AndroidX.Arch.Core.Common => 0x47c7b4fa => 185
	i32 1208641965, ; 122: System.Diagnostics.Process => 0x480a69ad => 29
	i32 1219128291, ; 123: System.IO.IsolatedStorage => 0x48aa6be3 => 52
	i32 1253011324, ; 124: Microsoft.Win32.Registry => 0x4aaf6f7c => 5
	i32 1264511973, ; 125: Xamarin.AndroidX.Startup.StartupRuntime.dll => 0x4b5eebe5 => 196
	i32 1275534314, ; 126: Xamarin.KotlinX.Coroutines.Android => 0x4c071bea => 208
	i32 1278448581, ; 127: Xamarin.AndroidX.Annotation.Jvm => 0x4c3393c5 => 184
	i32 1309188875, ; 128: System.Private.DataContractSerialization => 0x4e08a30b => 85
	i32 1324164729, ; 129: System.Linq => 0x4eed2679 => 61
	i32 1335329327, ; 130: System.Runtime.Serialization.Json.dll => 0x4f97822f => 112
	i32 1364015309, ; 131: System.IO => 0x514d38cd => 57
	i32 1379779777, ; 132: System.Resources.ResourceManager => 0x523dc4c1 => 99
	i32 1402170036, ; 133: System.Configuration.dll => 0x53936ab4 => 19
	i32 1408764838, ; 134: System.Runtime.Serialization.Formatters.dll => 0x53f80ba6 => 111
	i32 1411638395, ; 135: System.Runtime.CompilerServices.Unsafe => 0x5423e47b => 101
	i32 1422545099, ; 136: System.Runtime.CompilerServices.VisualC => 0x54ca50cb => 102
	i32 1434145427, ; 137: System.Runtime.Handles => 0x557b5293 => 104
	i32 1435222561, ; 138: Xamarin.Google.Crypto.Tink.Android.dll => 0x558bc221 => 200
	i32 1439761251, ; 139: System.Net.Quic.dll => 0x55d10363 => 71
	i32 1452070440, ; 140: System.Formats.Asn1.dll => 0x568cd628 => 38
	i32 1453312822, ; 141: System.Diagnostics.Tools.dll => 0x569fcb36 => 32
	i32 1457743152, ; 142: System.Runtime.Extensions.dll => 0x56e36530 => 103
	i32 1458022317, ; 143: System.Net.Security.dll => 0x56e7a7ad => 73
	i32 1461234159, ; 144: System.Collections.Immutable.dll => 0x5718a9ef => 9
	i32 1461719063, ; 145: System.Security.Cryptography.OpenSsl => 0x57201017 => 123
	i32 1462112819, ; 146: System.IO.Compression.dll => 0x57261233 => 46
	i32 1479771757, ; 147: System.Collections.Immutable => 0x5833866d => 9
	i32 1480492111, ; 148: System.IO.Compression.Brotli.dll => 0x583e844f => 43
	i32 1487239319, ; 149: Microsoft.Win32.Primitives => 0x58a57897 => 4
	i32 1536373174, ; 150: System.Diagnostics.TextWriterTraceListener => 0x5b9331b6 => 31
	i32 1543031311, ; 151: System.Text.RegularExpressions.dll => 0x5bf8ca0f => 138
	i32 1543355203, ; 152: System.Reflection.Emit.dll => 0x5bfdbb43 => 92
	i32 1550322496, ; 153: System.Reflection.Extensions.dll => 0x5c680b40 => 93
	i32 1565862583, ; 154: System.IO.FileSystem.Primitives => 0x5d552ab7 => 49
	i32 1566207040, ; 155: System.Threading.Tasks.Dataflow.dll => 0x5d5a6c40 => 141
	i32 1573704789, ; 156: System.Runtime.Serialization.Json => 0x5dccd455 => 112
	i32 1580037396, ; 157: System.Threading.Overlapped => 0x5e2d7514 => 140
	i32 1592978981, ; 158: System.Runtime.Serialization.dll => 0x5ef2ee25 => 115
	i32 1597949149, ; 159: Xamarin.Google.ErrorProne.Annotations => 0x5f3ec4dd => 201
	i32 1601112923, ; 160: System.Xml.Serialization => 0x5f6f0b5b => 157
	i32 1604827217, ; 161: System.Net.WebClient => 0x5fa7b851 => 76
	i32 1618516317, ; 162: System.Net.WebSockets.Client.dll => 0x6078995d => 79
	i32 1622358360, ; 163: System.Dynamic.Runtime => 0x60b33958 => 37
	i32 1639515021, ; 164: System.Net.Http.dll => 0x61b9038d => 64
	i32 1639986890, ; 165: System.Text.RegularExpressions => 0x61c036ca => 138
	i32 1641389582, ; 166: System.ComponentModel.EventBasedAsync.dll => 0x61d59e0e => 15
	i32 1657153582, ; 167: System.Runtime => 0x62c6282e => 116
	i32 1658241508, ; 168: Xamarin.AndroidX.Tracing.Tracing.dll => 0x62d6c1e4 => 197
	i32 1675553242, ; 169: System.IO.FileSystem.DriveInfo.dll => 0x63dee9da => 48
	i32 1677501392, ; 170: System.Net.Primitives.dll => 0x63fca3d0 => 70
	i32 1678508291, ; 171: System.Net.WebSockets => 0x640c0103 => 80
	i32 1679769178, ; 172: System.Security.Cryptography => 0x641f3e5a => 126
	i32 1691477237, ; 173: System.Reflection.Metadata => 0x64d1e4f5 => 94
	i32 1696967625, ; 174: System.Security.Cryptography.Csp => 0x6525abc9 => 121
	i32 1698840827, ; 175: Xamarin.Kotlin.StdLib.Common => 0x654240fb => 205
	i32 1701541528, ; 176: System.Diagnostics.Debug.dll => 0x656b7698 => 26
	i32 1726116996, ; 177: System.Reflection.dll => 0x66e27484 => 97
	i32 1728033016, ; 178: System.Diagnostics.FileVersionInfo.dll => 0x66ffb0f8 => 28
	i32 1744077396, ; 179: MonoGame.Extended.Graphics.dll => 0x67f48254 => 178
	i32 1744735666, ; 180: System.Transactions.Local.dll => 0x67fe8db2 => 149
	i32 1746316138, ; 181: Mono.Android.Export => 0x6816ab6a => 169
	i32 1750313021, ; 182: Microsoft.Win32.Primitives.dll => 0x6853a83d => 4
	i32 1758240030, ; 183: System.Resources.Reader.dll => 0x68cc9d1e => 98
	i32 1763938596, ; 184: System.Diagnostics.TraceSource.dll => 0x69239124 => 33
	i32 1765942094, ; 185: System.Reflection.Extensions => 0x6942234e => 93
	i32 1776026572, ; 186: System.Core.dll => 0x69dc03cc => 21
	i32 1777075843, ; 187: System.Globalization.Extensions.dll => 0x69ec0683 => 41
	i32 1780572499, ; 188: Mono.Android.Runtime.dll => 0x6a216153 => 170
	i32 1813058853, ; 189: Xamarin.Kotlin.StdLib.dll => 0x6c111525 => 204
	i32 1818787751, ; 190: Microsoft.VisualBasic.Core => 0x6c687fa7 => 2
	i32 1824175904, ; 191: System.Text.Encoding.Extensions => 0x6cbab720 => 134
	i32 1824722060, ; 192: System.Runtime.Serialization.Formatters => 0x6cc30c8c => 111
	i32 1858542181, ; 193: System.Linq.Expressions => 0x6ec71a65 => 58
	i32 1870277092, ; 194: System.Reflection.Primitives => 0x6f7a29e4 => 95
	i32 1879696579, ; 195: System.Formats.Tar.dll => 0x7009e4c3 => 39
	i32 1885316902, ; 196: Xamarin.AndroidX.Arch.Core.Runtime.dll => 0x705fa726 => 186
	i32 1888955245, ; 197: System.Diagnostics.Contracts => 0x70972b6d => 25
	i32 1889954781, ; 198: System.Reflection.Metadata.dll => 0x70a66bdd => 94
	i32 1898237753, ; 199: System.Reflection.DispatchProxy => 0x7124cf39 => 89
	i32 1900610850, ; 200: System.Resources.ResourceManager.dll => 0x71490522 => 99
	i32 1910275211, ; 201: System.Collections.NonGeneric.dll => 0x71dc7c8b => 10
	i32 1939592360, ; 202: System.Private.Xml.Linq => 0x739bd4a8 => 87
	i32 1956758971, ; 203: System.Resources.Writer => 0x74a1c5bb => 100
	i32 1961813231, ; 204: Xamarin.AndroidX.Security.SecurityCrypto.dll => 0x74eee4ef => 195
	i32 1983156543, ; 205: Xamarin.Kotlin.StdLib.Common.dll => 0x7634913f => 205
	i32 2011961780, ; 206: System.Buffers.dll => 0x77ec19b4 => 7
	i32 2045470958, ; 207: System.Private.Xml => 0x79eb68ee => 88
	i32 2060060697, ; 208: System.Windows.dll => 0x7aca0819 => 154
	i32 2070888862, ; 209: System.Diagnostics.TraceSource => 0x7b6f419e => 33
	i32 2079903147, ; 210: System.Runtime.dll => 0x7bf8cdab => 116
	i32 2090596640, ; 211: System.Numerics.Vectors => 0x7c9bf920 => 82
	i32 2127167465, ; 212: System.Console => 0x7ec9ffe9 => 20
	i32 2142473426, ; 213: System.Collections.Specialized => 0x7fb38cd2 => 11
	i32 2143790110, ; 214: System.Xml.XmlSerializer.dll => 0x7fc7a41e => 162
	i32 2146852085, ; 215: Microsoft.VisualBasic.dll => 0x7ff65cf5 => 3
	i32 2193016926, ; 216: System.ObjectModel.dll => 0x82b6c85e => 84
	i32 2197552240, ; 217: MonoGame.Extended.dll => 0x82fbfc70 => 177
	i32 2201107256, ; 218: Xamarin.KotlinX.Coroutines.Core.Jvm.dll => 0x83323b38 => 209
	i32 2201231467, ; 219: System.Net.Http => 0x8334206b => 64
	i32 2222056684, ; 220: System.Threading.Tasks.Parallel => 0x8471e4ec => 143
	i32 2252106437, ; 221: System.Xml.Serialization.dll => 0x863c6ac5 => 157
	i32 2256313426, ; 222: System.Globalization.Extensions => 0x867c9c52 => 41
	i32 2265110946, ; 223: System.Security.AccessControl.dll => 0x8702d9a2 => 117
	i32 2293034957, ; 224: System.ServiceModel.Web.dll => 0x88acefcd => 131
	i32 2295906218, ; 225: System.Net.Sockets => 0x88d8bfaa => 75
	i32 2298471582, ; 226: System.Net.Mail => 0x88ffe49e => 66
	i32 2305521784, ; 227: System.Private.CoreLib.dll => 0x896b7878 => 172
	i32 2315684594, ; 228: Xamarin.AndroidX.Annotation.dll => 0x8a068af2 => 182
	i32 2320631194, ; 229: System.Threading.Tasks.Parallel.dll => 0x8a52059a => 143
	i32 2340441535, ; 230: System.Runtime.InteropServices.RuntimeInformation.dll => 0x8b804dbf => 106
	i32 2344264397, ; 231: System.ValueTuple => 0x8bbaa2cd => 151
	i32 2346232420, ; 232: MonoGame.Extended.Tiled.dll => 0x8bd8aa64 => 179
	i32 2353062107, ; 233: System.Net.Primitives => 0x8c40e0db => 70
	i32 2368005991, ; 234: System.Xml.ReaderWriter.dll => 0x8d24e767 => 156
	i32 2378619854, ; 235: System.Security.Cryptography.Csp.dll => 0x8dc6dbce => 121
	i32 2383496789, ; 236: System.Security.Principal.Windows.dll => 0x8e114655 => 127
	i32 2401565422, ; 237: System.Web.HttpUtility => 0x8f24faee => 152
	i32 2421380589, ; 238: System.Threading.Tasks.Dataflow => 0x905355ed => 141
	i32 2435356389, ; 239: System.Console.dll => 0x912896e5 => 20
	i32 2435904999, ; 240: System.ComponentModel.DataAnnotations.dll => 0x9130f5e7 => 14
	i32 2454642406, ; 241: System.Text.Encoding.dll => 0x924edee6 => 135
	i32 2458678730, ; 242: System.Net.Sockets.dll => 0x928c75ca => 75
	i32 2459001652, ; 243: System.Linq.Parallel.dll => 0x92916334 => 59
	i32 2471841756, ; 244: netstandard.dll => 0x93554fdc => 167
	i32 2475788418, ; 245: Java.Interop.dll => 0x93918882 => 168
	i32 2483903535, ; 246: System.ComponentModel.EventBasedAsync => 0x940d5c2f => 15
	i32 2484371297, ; 247: System.Net.ServicePoint => 0x94147f61 => 74
	i32 2490993605, ; 248: System.AppContext.dll => 0x94798bc5 => 6
	i32 2501346920, ; 249: System.Data.DataSetExtensions => 0x95178668 => 23
	i32 2505896520, ; 250: Xamarin.AndroidX.Lifecycle.Runtime.dll => 0x955cf248 => 193
	i32 2538310050, ; 251: System.Reflection.Emit.Lightweight.dll => 0x974b89a2 => 91
	i32 2562349572, ; 252: Microsoft.CSharp => 0x98ba5a04 => 1
	i32 2570120770, ; 253: System.Text.Encodings.Web => 0x9930ee42 => 136
	i32 2585220780, ; 254: System.Text.Encoding.Extensions.dll => 0x9a1756ac => 134
	i32 2585805581, ; 255: System.Net.Ping => 0x9a20430d => 69
	i32 2589602615, ; 256: System.Threading.ThreadPool => 0x9a5a3337 => 146
	i32 2605712449, ; 257: Xamarin.KotlinX.Coroutines.Core.Jvm => 0x9b500441 => 209
	i32 2617129537, ; 258: System.Private.Xml.dll => 0x9bfe3a41 => 88
	i32 2618712057, ; 259: System.Reflection.TypeExtensions.dll => 0x9c165ff9 => 96
	i32 2627185994, ; 260: System.Diagnostics.TextWriterTraceListener.dll => 0x9c97ad4a => 31
	i32 2629843544, ; 261: System.IO.Compression.ZipFile.dll => 0x9cc03a58 => 45
	i32 2663698177, ; 262: System.Runtime.Loader => 0x9ec4cf01 => 109
	i32 2664396074, ; 263: System.Xml.XDocument.dll => 0x9ecf752a => 158
	i32 2665622720, ; 264: System.Drawing.Primitives => 0x9ee22cc0 => 35
	i32 2676780864, ; 265: System.Data.Common.dll => 0x9f8c6f40 => 22
	i32 2686887180, ; 266: System.Runtime.Serialization.Xml.dll => 0xa026a50c => 114
	i32 2693849962, ; 267: System.IO.dll => 0xa090e36a => 57
	i32 2701096212, ; 268: Xamarin.AndroidX.Tracing.Tracing => 0xa0ff7514 => 197
	i32 2715334215, ; 269: System.Threading.Tasks.dll => 0xa1d8b647 => 144
	i32 2716630119, ; 270: MonoGame.Extended.Graphics => 0xa1ec7c67 => 178
	i32 2717744543, ; 271: System.Security.Claims => 0xa1fd7d9f => 118
	i32 2719963679, ; 272: System.Security.Cryptography.Cng.dll => 0xa21f5a1f => 120
	i32 2724373263, ; 273: System.Runtime.Numerics.dll => 0xa262a30f => 110
	i32 2735172069, ; 274: System.Threading.Channels => 0xa30769e5 => 139
	i32 2740948882, ; 275: System.IO.Pipes.AccessControl => 0xa35f8f92 => 54
	i32 2748088231, ; 276: System.Runtime.InteropServices.JavaScript => 0xa3cc7fa7 => 105
	i32 2765824710, ; 277: System.Text.Encoding.CodePages.dll => 0xa4db22c6 => 133
	i32 2770495804, ; 278: Xamarin.Jetbrains.Annotations.dll => 0xa522693c => 203
	i32 2801831435, ; 279: Microsoft.Maui.Graphics => 0xa7008e0b => 176
	i32 2803228030, ; 280: System.Xml.XPath.XDocument.dll => 0xa715dd7e => 159
	i32 2819470561, ; 281: System.Xml.dll => 0xa80db4e1 => 163
	i32 2821205001, ; 282: System.ServiceProcess.dll => 0xa8282c09 => 132
	i32 2824502124, ; 283: System.Xml.XmlDocument => 0xa85a7b6c => 161
	i32 2849599387, ; 284: System.Threading.Overlapped.dll => 0xa9d96f9b => 140
	i32 2861098320, ; 285: Mono.Android.Export.dll => 0xaa88e550 => 169
	i32 2861189240, ; 286: Microsoft.Maui.Essentials => 0xaa8a4878 => 175
	i32 2875164099, ; 287: Jsr305Binding.dll => 0xab5f85c3 => 199
	i32 2875220617, ; 288: System.Globalization.Calendars.dll => 0xab606289 => 40
	i32 2887636118, ; 289: System.Net.dll => 0xac1dd496 => 81
	i32 2899753641, ; 290: System.IO.UnmanagedMemoryStream => 0xacd6baa9 => 56
	i32 2900621748, ; 291: System.Dynamic.Runtime.dll => 0xace3f9b4 => 37
	i32 2901442782, ; 292: System.Reflection => 0xacf080de => 97
	i32 2905242038, ; 293: mscorlib.dll => 0xad2a79b6 => 166
	i32 2909740682, ; 294: System.Private.CoreLib => 0xad6f1e8a => 172
	i32 2919462931, ; 295: System.Numerics.Vectors.dll => 0xae037813 => 82
	i32 2921128767, ; 296: Xamarin.AndroidX.Annotation.Experimental.dll => 0xae1ce33f => 183
	i32 2936416060, ; 297: System.Resources.Reader => 0xaf06273c => 98
	i32 2940926066, ; 298: System.Diagnostics.StackTrace.dll => 0xaf4af872 => 30
	i32 2942453041, ; 299: System.Xml.XPath.XDocument => 0xaf624531 => 159
	i32 2959614098, ; 300: System.ComponentModel.dll => 0xb0682092 => 18
	i32 2968338931, ; 301: System.Security.Principal.Windows => 0xb0ed41f3 => 127
	i32 2972252294, ; 302: System.Security.Cryptography.Algorithms.dll => 0xb128f886 => 119
	i32 2987532451, ; 303: Xamarin.AndroidX.Security.SecurityCrypto => 0xb21220a3 => 195
	i32 3016983068, ; 304: Xamarin.AndroidX.Startup.StartupRuntime => 0xb3d3821c => 196
	i32 3023353419, ; 305: WindowsBase.dll => 0xb434b64b => 165
	i32 3038032645, ; 306: _Microsoft.Android.Resource.Designer.dll => 0xb514b305 => 211
	i32 3059408633, ; 307: Mono.Android.Runtime => 0xb65adef9 => 170
	i32 3059793426, ; 308: System.ComponentModel.Primitives => 0xb660be12 => 16
	i32 3075834255, ; 309: System.Threading.Tasks => 0xb755818f => 144
	i32 3090735792, ; 310: System.Security.Cryptography.X509Certificates.dll => 0xb838e2b0 => 125
	i32 3099732863, ; 311: System.Security.Claims.dll => 0xb8c22b7f => 118
	i32 3103600923, ; 312: System.Formats.Asn1 => 0xb8fd311b => 38
	i32 3111772706, ; 313: System.Runtime.Serialization => 0xb979e222 => 115
	i32 3121463068, ; 314: System.IO.FileSystem.AccessControl.dll => 0xba0dbf1c => 47
	i32 3124832203, ; 315: System.Threading.Tasks.Extensions => 0xba4127cb => 142
	i32 3132293585, ; 316: System.Security.AccessControl => 0xbab301d1 => 117
	i32 3144447155, ; 317: Autofac.dll => 0xbb6c74b3 => 173
	i32 3147165239, ; 318: System.Diagnostics.Tracing.dll => 0xbb95ee37 => 34
	i32 3148237826, ; 319: GoogleGson.dll => 0xbba64c02 => 174
	i32 3159123045, ; 320: System.Reflection.Primitives.dll => 0xbc4c6465 => 95
	i32 3160747431, ; 321: System.IO.MemoryMappedFiles => 0xbc652da7 => 53
	i32 3192346100, ; 322: System.Security.SecureString => 0xbe4755f4 => 129
	i32 3193515020, ; 323: System.Web => 0xbe592c0c => 153
	i32 3204380047, ; 324: System.Data.dll => 0xbefef58f => 24
	i32 3209718065, ; 325: System.Xml.XmlDocument.dll => 0xbf506931 => 161
	i32 3220365878, ; 326: System.Threading => 0xbff2e236 => 148
	i32 3226221578, ; 327: System.Runtime.Handles.dll => 0xc04c3c0a => 104
	i32 3251039220, ; 328: System.Reflection.DispatchProxy.dll => 0xc1c6ebf4 => 89
	i32 3265493905, ; 329: System.Linq.Queryable.dll => 0xc2a37b91 => 60
	i32 3265893370, ; 330: System.Threading.Tasks.Extensions.dll => 0xc2a993fa => 142
	i32 3277815716, ; 331: System.Resources.Writer.dll => 0xc35f7fa4 => 100
	i32 3279906254, ; 332: Microsoft.Win32.Registry.dll => 0xc37f65ce => 5
	i32 3280506390, ; 333: System.ComponentModel.Annotations.dll => 0xc3888e16 => 13
	i32 3290767353, ; 334: System.Security.Cryptography.Encoding => 0xc4251ff9 => 122
	i32 3299363146, ; 335: System.Text.Encoding => 0xc4a8494a => 135
	i32 3303498502, ; 336: System.Diagnostics.FileVersionInfo => 0xc4e76306 => 28
	i32 3316684772, ; 337: System.Net.Requests.dll => 0xc5b097e4 => 72
	i32 3317144872, ; 338: System.Data => 0xc5b79d28 => 24
	i32 3340431453, ; 339: Xamarin.AndroidX.Arch.Core.Runtime => 0xc71af05d => 186
	i32 3345895724, ; 340: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller.dll => 0xc76e512c => 194
	i32 3358260929, ; 341: System.Text.Json => 0xc82afec1 => 137
	i32 3362522851, ; 342: Xamarin.AndroidX.Core => 0xc86c06e3 => 190
	i32 3366347497, ; 343: Java.Interop => 0xc8a662e9 => 168
	i32 3395150330, ; 344: System.Runtime.CompilerServices.Unsafe.dll => 0xca5de1fa => 101
	i32 3403906625, ; 345: System.Security.Cryptography.OpenSsl.dll => 0xcae37e41 => 123
	i32 3429136800, ; 346: System.Xml => 0xcc6479a0 => 163
	i32 3430777524, ; 347: netstandard => 0xcc7d82b4 => 167
	i32 3445260447, ; 348: System.Formats.Tar => 0xcd5a809f => 39
	i32 3471940407, ; 349: System.ComponentModel.TypeConverter.dll => 0xcef19b37 => 17
	i32 3476120550, ; 350: Mono.Android => 0xcf3163e6 => 171
	i32 3485117614, ; 351: System.Text.Json.dll => 0xcfbaacae => 137
	i32 3486566296, ; 352: System.Transactions => 0xcfd0c798 => 150
	i32 3493954962, ; 353: Xamarin.AndroidX.Concurrent.Futures.dll => 0xd0418592 => 189
	i32 3509114376, ; 354: System.Xml.Linq => 0xd128d608 => 155
	i32 3515174580, ; 355: System.Security.dll => 0xd1854eb4 => 130
	i32 3530912306, ; 356: System.Configuration => 0xd2757232 => 19
	i32 3539954161, ; 357: System.Net.HttpListener => 0xd2ff69f1 => 65
	i32 3560100363, ; 358: System.Threading.Timer => 0xd432d20b => 147
	i32 3570554715, ; 359: System.IO.FileSystem.AccessControl => 0xd4d2575b => 47
	i32 3598340787, ; 360: System.Net.WebSockets.Client => 0xd67a52b3 => 79
	i32 3608519521, ; 361: System.Linq.dll => 0xd715a361 => 61
	i32 3624195450, ; 362: System.Runtime.InteropServices.RuntimeInformation => 0xd804d57a => 106
	i32 3633644679, ; 363: Xamarin.AndroidX.Annotation.Experimental => 0xd8950487 => 183
	i32 3638274909, ; 364: System.IO.FileSystem.Primitives.dll => 0xd8dbab5d => 49
	i32 3645089577, ; 365: System.ComponentModel.DataAnnotations => 0xd943a729 => 14
	i32 3660523487, ; 366: System.Net.NetworkInformation => 0xda2f27df => 68
	i32 3672681054, ; 367: Mono.Android.dll => 0xdae8aa5e => 171
	i32 3682565725, ; 368: Xamarin.AndroidX.Browser => 0xdb7f7e5d => 187
	i32 3684561358, ; 369: Xamarin.AndroidX.Concurrent.Futures => 0xdb9df1ce => 189
	i32 3700866549, ; 370: System.Net.WebProxy.dll => 0xdc96bdf5 => 78
	i32 3716563718, ; 371: System.Runtime.Intrinsics => 0xdd864306 => 108
	i32 3718780102, ; 372: Xamarin.AndroidX.Annotation => 0xdda814c6 => 182
	i32 3732100267, ; 373: System.Net.NameResolution => 0xde7354ab => 67
	i32 3737834244, ; 374: System.Net.Http.Json.dll => 0xdecad304 => 63
	i32 3748608112, ; 375: System.Diagnostics.DiagnosticSource => 0xdf6f3870 => 27
	i32 3751444290, ; 376: System.Xml.XPath => 0xdf9a7f42 => 160
	i32 3786282454, ; 377: Xamarin.AndroidX.Collection => 0xe1ae15d6 => 188
	i32 3792276235, ; 378: System.Collections.NonGeneric => 0xe2098b0b => 10
	i32 3802395368, ; 379: System.Collections.Specialized.dll => 0xe2a3f2e8 => 11
	i32 3819260425, ; 380: System.Net.WebProxy => 0xe3a54a09 => 78
	i32 3823082795, ; 381: System.Security.Cryptography.dll => 0xe3df9d2b => 126
	i32 3829621856, ; 382: System.Numerics.dll => 0xe4436460 => 83
	i32 3831343120, ; 383: MonoGame.Framework.dll => 0xe45da810 => 180
	i32 3844307129, ; 384: System.Net.Mail.dll => 0xe52378b9 => 66
	i32 3849253459, ; 385: System.Runtime.InteropServices.dll => 0xe56ef253 => 107
	i32 3870376305, ; 386: System.Net.HttpListener.dll => 0xe6b14171 => 65
	i32 3873536506, ; 387: System.Security.Principal => 0xe6e179fa => 128
	i32 3875112723, ; 388: System.Security.Cryptography.Encoding.dll => 0xe6f98713 => 122
	i32 3882769740, ; 389: MonoGame.Extended => 0xe76e5d4c => 177
	i32 3885497537, ; 390: System.Net.WebHeaderCollection.dll => 0xe797fcc1 => 77
	i32 3888767677, ; 391: Xamarin.AndroidX.ProfileInstaller.ProfileInstaller => 0xe7c9e2bd => 194
	i32 3896106733, ; 392: System.Collections.Concurrent.dll => 0xe839deed => 8
	i32 3896760992, ; 393: Xamarin.AndroidX.Core.dll => 0xe843daa0 => 190
	i32 3901907137, ; 394: Microsoft.VisualBasic.Core.dll => 0xe89260c1 => 2
	i32 3920810846, ; 395: System.IO.Compression.FileSystem.dll => 0xe9b2d35e => 44
	i32 3921031405, ; 396: Xamarin.AndroidX.VersionedParcelable.dll => 0xe9b630ed => 198
	i32 3928044579, ; 397: System.Xml.ReaderWriter => 0xea213423 => 156
	i32 3930554604, ; 398: System.Security.Principal.dll => 0xea4780ec => 128
	i32 3945713374, ; 399: System.Data.DataSetExtensions.dll => 0xeb2ecede => 23
	i32 3953953790, ; 400: System.Text.Encoding.CodePages => 0xebac8bfe => 133
	i32 4003436829, ; 401: System.Diagnostics.Process.dll => 0xee9f991d => 29
	i32 4015948917, ; 402: Xamarin.AndroidX.Annotation.Jvm.dll => 0xef5e8475 => 184
	i32 4025784931, ; 403: System.Memory => 0xeff49a63 => 62
	i32 4054681211, ; 404: System.Reflection.Emit.ILGeneration => 0xf1ad867b => 90
	i32 4068434129, ; 405: System.Private.Xml.Linq.dll => 0xf27f60d1 => 87
	i32 4073602200, ; 406: System.Threading.dll => 0xf2ce3c98 => 148
	i32 4094352644, ; 407: Microsoft.Maui.Essentials.dll => 0xf40add04 => 175
	i32 4099507663, ; 408: System.Drawing.dll => 0xf45985cf => 36
	i32 4100113165, ; 409: System.Private.Uri => 0xf462c30d => 86
	i32 4127667938, ; 410: System.IO.FileSystem.Watcher => 0xf60736e2 => 50
	i32 4130442656, ; 411: System.AppContext => 0xf6318da0 => 6
	i32 4147896353, ; 412: System.Reflection.Emit.ILGeneration.dll => 0xf73be021 => 90
	i32 4151237749, ; 413: System.Core => 0xf76edc75 => 21
	i32 4159265925, ; 414: System.Xml.XmlSerializer => 0xf7e95c85 => 162
	i32 4161255271, ; 415: System.Reflection.TypeExtensions => 0xf807b767 => 96
	i32 4164802419, ; 416: System.IO.FileSystem.Watcher.dll => 0xf83dd773 => 50
	i32 4181436372, ; 417: System.Runtime.Serialization.Primitives => 0xf93ba7d4 => 113
	i32 4185676441, ; 418: System.Security => 0xf97c5a99 => 130
	i32 4196529839, ; 419: System.Net.WebClient.dll => 0xfa21f6af => 76
	i32 4213026141, ; 420: System.Diagnostics.DiagnosticSource.dll => 0xfb1dad5d => 27
	i32 4232695587, ; 421: Android.dll => 0xfc49cf23 => 0
	i32 4260525087, ; 422: System.Buffers => 0xfdf2741f => 7
	i32 4274976490 ; 423: System.Runtime.Numerics => 0xfecef6ea => 110
], align 4

@assembly_image_cache_indices = dso_local local_unnamed_addr constant [424 x i32] [
	i32 68, ; 0
	i32 67, ; 1
	i32 108, ; 2
	i32 193, ; 3
	i32 202, ; 4
	i32 48, ; 5
	i32 181, ; 6
	i32 80, ; 7
	i32 145, ; 8
	i32 30, ; 9
	i32 124, ; 10
	i32 176, ; 11
	i32 102, ; 12
	i32 107, ; 13
	i32 139, ; 14
	i32 206, ; 15
	i32 77, ; 16
	i32 124, ; 17
	i32 13, ; 18
	i32 188, ; 19
	i32 132, ; 20
	i32 151, ; 21
	i32 18, ; 22
	i32 187, ; 23
	i32 26, ; 24
	i32 1, ; 25
	i32 59, ; 26
	i32 42, ; 27
	i32 91, ; 28
	i32 147, ; 29
	i32 191, ; 30
	i32 54, ; 31
	i32 69, ; 32
	i32 83, ; 33
	i32 192, ; 34
	i32 173, ; 35
	i32 131, ; 36
	i32 55, ; 37
	i32 149, ; 38
	i32 74, ; 39
	i32 145, ; 40
	i32 62, ; 41
	i32 146, ; 42
	i32 211, ; 43
	i32 165, ; 44
	i32 12, ; 45
	i32 125, ; 46
	i32 152, ; 47
	i32 113, ; 48
	i32 166, ; 49
	i32 164, ; 50
	i32 191, ; 51
	i32 84, ; 52
	i32 180, ; 53
	i32 150, ; 54
	i32 206, ; 55
	i32 60, ; 56
	i32 51, ; 57
	i32 103, ; 58
	i32 114, ; 59
	i32 40, ; 60
	i32 199, ; 61
	i32 0, ; 62
	i32 120, ; 63
	i32 52, ; 64
	i32 44, ; 65
	i32 119, ; 66
	i32 81, ; 67
	i32 136, ; 68
	i32 198, ; 69
	i32 185, ; 70
	i32 8, ; 71
	i32 73, ; 72
	i32 155, ; 73
	i32 208, ; 74
	i32 154, ; 75
	i32 92, ; 76
	i32 203, ; 77
	i32 45, ; 78
	i32 207, ; 79
	i32 109, ; 80
	i32 129, ; 81
	i32 25, ; 82
	i32 72, ; 83
	i32 55, ; 84
	i32 46, ; 85
	i32 210, ; 86
	i32 179, ; 87
	i32 22, ; 88
	i32 86, ; 89
	i32 43, ; 90
	i32 160, ; 91
	i32 71, ; 92
	i32 3, ; 93
	i32 42, ; 94
	i32 63, ; 95
	i32 16, ; 96
	i32 53, ; 97
	i32 202, ; 98
	i32 105, ; 99
	i32 181, ; 100
	i32 207, ; 101
	i32 200, ; 102
	i32 192, ; 103
	i32 34, ; 104
	i32 158, ; 105
	i32 85, ; 106
	i32 32, ; 107
	i32 12, ; 108
	i32 51, ; 109
	i32 210, ; 110
	i32 56, ; 111
	i32 36, ; 112
	i32 201, ; 113
	i32 35, ; 114
	i32 58, ; 115
	i32 174, ; 116
	i32 17, ; 117
	i32 204, ; 118
	i32 164, ; 119
	i32 153, ; 120
	i32 185, ; 121
	i32 29, ; 122
	i32 52, ; 123
	i32 5, ; 124
	i32 196, ; 125
	i32 208, ; 126
	i32 184, ; 127
	i32 85, ; 128
	i32 61, ; 129
	i32 112, ; 130
	i32 57, ; 131
	i32 99, ; 132
	i32 19, ; 133
	i32 111, ; 134
	i32 101, ; 135
	i32 102, ; 136
	i32 104, ; 137
	i32 200, ; 138
	i32 71, ; 139
	i32 38, ; 140
	i32 32, ; 141
	i32 103, ; 142
	i32 73, ; 143
	i32 9, ; 144
	i32 123, ; 145
	i32 46, ; 146
	i32 9, ; 147
	i32 43, ; 148
	i32 4, ; 149
	i32 31, ; 150
	i32 138, ; 151
	i32 92, ; 152
	i32 93, ; 153
	i32 49, ; 154
	i32 141, ; 155
	i32 112, ; 156
	i32 140, ; 157
	i32 115, ; 158
	i32 201, ; 159
	i32 157, ; 160
	i32 76, ; 161
	i32 79, ; 162
	i32 37, ; 163
	i32 64, ; 164
	i32 138, ; 165
	i32 15, ; 166
	i32 116, ; 167
	i32 197, ; 168
	i32 48, ; 169
	i32 70, ; 170
	i32 80, ; 171
	i32 126, ; 172
	i32 94, ; 173
	i32 121, ; 174
	i32 205, ; 175
	i32 26, ; 176
	i32 97, ; 177
	i32 28, ; 178
	i32 178, ; 179
	i32 149, ; 180
	i32 169, ; 181
	i32 4, ; 182
	i32 98, ; 183
	i32 33, ; 184
	i32 93, ; 185
	i32 21, ; 186
	i32 41, ; 187
	i32 170, ; 188
	i32 204, ; 189
	i32 2, ; 190
	i32 134, ; 191
	i32 111, ; 192
	i32 58, ; 193
	i32 95, ; 194
	i32 39, ; 195
	i32 186, ; 196
	i32 25, ; 197
	i32 94, ; 198
	i32 89, ; 199
	i32 99, ; 200
	i32 10, ; 201
	i32 87, ; 202
	i32 100, ; 203
	i32 195, ; 204
	i32 205, ; 205
	i32 7, ; 206
	i32 88, ; 207
	i32 154, ; 208
	i32 33, ; 209
	i32 116, ; 210
	i32 82, ; 211
	i32 20, ; 212
	i32 11, ; 213
	i32 162, ; 214
	i32 3, ; 215
	i32 84, ; 216
	i32 177, ; 217
	i32 209, ; 218
	i32 64, ; 219
	i32 143, ; 220
	i32 157, ; 221
	i32 41, ; 222
	i32 117, ; 223
	i32 131, ; 224
	i32 75, ; 225
	i32 66, ; 226
	i32 172, ; 227
	i32 182, ; 228
	i32 143, ; 229
	i32 106, ; 230
	i32 151, ; 231
	i32 179, ; 232
	i32 70, ; 233
	i32 156, ; 234
	i32 121, ; 235
	i32 127, ; 236
	i32 152, ; 237
	i32 141, ; 238
	i32 20, ; 239
	i32 14, ; 240
	i32 135, ; 241
	i32 75, ; 242
	i32 59, ; 243
	i32 167, ; 244
	i32 168, ; 245
	i32 15, ; 246
	i32 74, ; 247
	i32 6, ; 248
	i32 23, ; 249
	i32 193, ; 250
	i32 91, ; 251
	i32 1, ; 252
	i32 136, ; 253
	i32 134, ; 254
	i32 69, ; 255
	i32 146, ; 256
	i32 209, ; 257
	i32 88, ; 258
	i32 96, ; 259
	i32 31, ; 260
	i32 45, ; 261
	i32 109, ; 262
	i32 158, ; 263
	i32 35, ; 264
	i32 22, ; 265
	i32 114, ; 266
	i32 57, ; 267
	i32 197, ; 268
	i32 144, ; 269
	i32 178, ; 270
	i32 118, ; 271
	i32 120, ; 272
	i32 110, ; 273
	i32 139, ; 274
	i32 54, ; 275
	i32 105, ; 276
	i32 133, ; 277
	i32 203, ; 278
	i32 176, ; 279
	i32 159, ; 280
	i32 163, ; 281
	i32 132, ; 282
	i32 161, ; 283
	i32 140, ; 284
	i32 169, ; 285
	i32 175, ; 286
	i32 199, ; 287
	i32 40, ; 288
	i32 81, ; 289
	i32 56, ; 290
	i32 37, ; 291
	i32 97, ; 292
	i32 166, ; 293
	i32 172, ; 294
	i32 82, ; 295
	i32 183, ; 296
	i32 98, ; 297
	i32 30, ; 298
	i32 159, ; 299
	i32 18, ; 300
	i32 127, ; 301
	i32 119, ; 302
	i32 195, ; 303
	i32 196, ; 304
	i32 165, ; 305
	i32 211, ; 306
	i32 170, ; 307
	i32 16, ; 308
	i32 144, ; 309
	i32 125, ; 310
	i32 118, ; 311
	i32 38, ; 312
	i32 115, ; 313
	i32 47, ; 314
	i32 142, ; 315
	i32 117, ; 316
	i32 173, ; 317
	i32 34, ; 318
	i32 174, ; 319
	i32 95, ; 320
	i32 53, ; 321
	i32 129, ; 322
	i32 153, ; 323
	i32 24, ; 324
	i32 161, ; 325
	i32 148, ; 326
	i32 104, ; 327
	i32 89, ; 328
	i32 60, ; 329
	i32 142, ; 330
	i32 100, ; 331
	i32 5, ; 332
	i32 13, ; 333
	i32 122, ; 334
	i32 135, ; 335
	i32 28, ; 336
	i32 72, ; 337
	i32 24, ; 338
	i32 186, ; 339
	i32 194, ; 340
	i32 137, ; 341
	i32 190, ; 342
	i32 168, ; 343
	i32 101, ; 344
	i32 123, ; 345
	i32 163, ; 346
	i32 167, ; 347
	i32 39, ; 348
	i32 17, ; 349
	i32 171, ; 350
	i32 137, ; 351
	i32 150, ; 352
	i32 189, ; 353
	i32 155, ; 354
	i32 130, ; 355
	i32 19, ; 356
	i32 65, ; 357
	i32 147, ; 358
	i32 47, ; 359
	i32 79, ; 360
	i32 61, ; 361
	i32 106, ; 362
	i32 183, ; 363
	i32 49, ; 364
	i32 14, ; 365
	i32 68, ; 366
	i32 171, ; 367
	i32 187, ; 368
	i32 189, ; 369
	i32 78, ; 370
	i32 108, ; 371
	i32 182, ; 372
	i32 67, ; 373
	i32 63, ; 374
	i32 27, ; 375
	i32 160, ; 376
	i32 188, ; 377
	i32 10, ; 378
	i32 11, ; 379
	i32 78, ; 380
	i32 126, ; 381
	i32 83, ; 382
	i32 180, ; 383
	i32 66, ; 384
	i32 107, ; 385
	i32 65, ; 386
	i32 128, ; 387
	i32 122, ; 388
	i32 177, ; 389
	i32 77, ; 390
	i32 194, ; 391
	i32 8, ; 392
	i32 190, ; 393
	i32 2, ; 394
	i32 44, ; 395
	i32 198, ; 396
	i32 156, ; 397
	i32 128, ; 398
	i32 23, ; 399
	i32 133, ; 400
	i32 29, ; 401
	i32 184, ; 402
	i32 62, ; 403
	i32 90, ; 404
	i32 87, ; 405
	i32 148, ; 406
	i32 175, ; 407
	i32 36, ; 408
	i32 86, ; 409
	i32 50, ; 410
	i32 6, ; 411
	i32 90, ; 412
	i32 21, ; 413
	i32 162, ; 414
	i32 96, ; 415
	i32 50, ; 416
	i32 113, ; 417
	i32 130, ; 418
	i32 76, ; 419
	i32 27, ; 420
	i32 0, ; 421
	i32 7, ; 422
	i32 110 ; 423
], align 4

@marshal_methods_number_of_classes = dso_local local_unnamed_addr constant i32 0, align 4

@marshal_methods_class_cache = dso_local local_unnamed_addr global [0 x %struct.MarshalMethodsManagedClass] zeroinitializer, align 4

; Names of classes in which marshal methods reside
@mm_class_names = dso_local local_unnamed_addr constant [0 x ptr] zeroinitializer, align 4

@mm_method_names = dso_local local_unnamed_addr constant [1 x %struct.MarshalMethodName] [
	%struct.MarshalMethodName {
		i64 0, ; id 0x0; name: 
		ptr @.MarshalMethodName.0_name; char* name
	} ; 0
], align 8

; get_function_pointer (uint32_t mono_image_index, uint32_t class_index, uint32_t method_token, void*& target_ptr)
@get_function_pointer = internal dso_local unnamed_addr global ptr null, align 4

; Functions

; Function attributes: "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" uwtable willreturn
define void @xamarin_app_init(ptr nocapture noundef readnone %env, ptr noundef %fn) local_unnamed_addr #0
{
	%fnIsNull = icmp eq ptr %fn, null
	br i1 %fnIsNull, label %1, label %2

1: ; preds = %0
	%putsResult = call noundef i32 @puts(ptr @.str.0)
	call void @abort()
	unreachable 

2: ; preds = %1, %0
	store ptr %fn, ptr @get_function_pointer, align 4, !tbaa !3
	ret void
}

; Strings
@.str.0 = private unnamed_addr constant [40 x i8] c"get_function_pointer MUST be specified\0A\00", align 1

;MarshalMethodName
@.MarshalMethodName.0_name = private unnamed_addr constant [1 x i8] c"\00", align 1

; External functions

; Function attributes: noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8"
declare void @abort() local_unnamed_addr #2

; Function attributes: nofree nounwind
declare noundef i32 @puts(ptr noundef) local_unnamed_addr #1
attributes #0 = { "min-legal-vector-width"="0" mustprogress nofree norecurse nosync "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" uwtable willreturn }
attributes #1 = { nofree nounwind }
attributes #2 = { noreturn "no-trapping-math"="true" nounwind "stack-protector-buffer-size"="8" "stackrealign" "target-cpu"="i686" "target-features"="+cx8,+mmx,+sse,+sse2,+sse3,+ssse3,+x87" "tune-cpu"="generic" }

; Metadata
!llvm.module.flags = !{!0, !1, !7}
!0 = !{i32 1, !"wchar_size", i32 4}
!1 = !{i32 7, !"PIC Level", i32 2}
!llvm.ident = !{!2}
!2 = !{!"Xamarin.Android remotes/origin/release/8.0.1xx @ f1b7113872c8db3dfee70d11925e81bb752dc8d0"}
!3 = !{!4, !4, i64 0}
!4 = !{!"any pointer", !5, i64 0}
!5 = !{!"omnipotent char", !6, i64 0}
!6 = !{!"Simple C++ TBAA"}
!7 = !{i32 1, !"NumRegisterParameters", i32 0}
