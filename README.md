# YunmaIcons.Avalonia

Lightweight SVG icon library for AvaloniaUI with 98+ icons, animations, and markup extension support.

## Installation

```bash
dotnet add package YunmaIcons.Avalonia
```

## Usage

```xml
xmlns:yi="clr-namespace:YunmaIcons.Avalonia;assembly=YunmaIcons.Avalonia"

<!-- Basic -->
<yi:AppIcon Kind="Home" Size="24" Foreground="White"/>

<!-- With animation -->
<yi:AppIcon Kind="RefreshCw" Size="20" Animate="Spin" Foreground="#4F8EF7"/>
<yi:AppIcon Kind="Bell" Size="20" Animate="Pulse" Foreground="#4CAF7D"/>
<yi:AppIcon Kind="Star" Size="20" Animate="Bounce" Foreground="#FFD700"/>

<!-- Markup extension -->
<Button Content="{yi:AppIcon Home}"/>
<Button Content="{yi:AppIcon Settings, 20}"/>

<!-- Custom stroke -->
<yi:AppIcon Kind="Heart" Size="32" StrokeWidth="1.5" Foreground="#FF6B6B"/>

<!-- Inherit foreground dari parent -->
<StackPanel Foreground="CornflowerBlue">
    <yi:AppIcon Kind="Bell" Size="20"/>
    <yi:AppIcon Kind="Search" Size="20"/>
</StackPanel>
```

## Available Icons (98+)

| Category      | Icons                                                                                                    |
| ------------- | -------------------------------------------------------------------------------------------------------- |
| Navigation    | Home, ArrowLeft, ArrowRight, ArrowUp, ArrowDown, ChevronLeft, ChevronRight, ChevronUp, ChevronDown, Menu |
| Actions       | Plus, Search, Edit, Trash, Copy, Save, Upload, Download, RefreshCw, Filter                               |
| Communication | Mail, Phone, PhoneCall, MessageSquare, Bell, Video, VideoOff, Mic, MicOff                                |
| Files         | File, FileText, Folder, Paperclip, Link, ExternalLink                                                    |
| UI            | Settings, Sliders, LayoutGrid, Ellipsis, MoreVertical, X, Check                                          |
| Media         | Camera, Image, Volume2, VolumeX, Radio, Sparkles                                                         |
| Status        | AlertCircle, AlertTriangle, Info, HelpCircle, Lock, Unlock, Eye, EyeOff                                  |
| People        | Users, Smile                                                                                             |
| Nature        | Sun, Moon, Cloud, CloudRain, Globe                                                                       |
| Business      | Building2, CreditCard, ShoppingCart, Package, Tag, Gift, BarChart2                                       |
| Device        | Smartphone, Wifi, WifiOff, Battery, BatteryCharging                                                      |
| Misc          | Heart, Star, Bookmark, Coffee, MapPin, Navigation, Compass, Map                                          |

## Animations

```xml
<yi:AppIcon Kind="RefreshCw" Animate="Spin"/>
<yi:AppIcon Kind="Bell" Animate="Pulse"/>
<yi:AppIcon Kind="Star" Animate="Bounce"/>
```

## License

MIT
