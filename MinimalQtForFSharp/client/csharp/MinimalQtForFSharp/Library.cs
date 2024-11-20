using Org.Whatever.MinimalQtForFSharp.Support;

namespace Org.Whatever.MinimalQtForFSharp;

public static class Library
{
    public static void Init()
    {
        if (NativeImplClient.Init() != 0)
        {
            Console.WriteLine("NativeImplClient.Init failed");
            return;
        }
        // registrations, static module inits
        Application.__Init();
        Common.__Init();
        Object.__Init();
        Layout.__Init();
        Color.__Init();
        PaintResources.__Init();
        Enums.__Init();
        PaintDevice.__Init();
        Image.__Init();
        Pixmap.__Init();
        Painter.__Init();
        Icon.__Init();
        KeySequence.__Init();
        Action.__Init();
        Region.__Init();
        Cursor.__Init();
        SizePolicy.__Init();
        Widget.__Init();
        BoxLayout.__Init();
        Date.__Init();
        CalendarWidget.__Init();
        Variant.__Init();
        ModelIndex.__Init();
        PersistentModelIndex.__Init();
        AbstractItemModel.__Init();
        ComboBox.__Init();
        Dialog.__Init();
        FileDialog.__Init();
        GridLayout.__Init();
        GroupBox.__Init();
        Frame.__Init();
        Label.__Init();
        LineEdit.__Init();
        AbstractListModel.__Init();
        AbstractScrollArea.__Init();
        AbstractItemDelegate.__Init();
        AbstractItemView.__Init();
        ListView.__Init();
        Menu.__Init();
        MenuBar.__Init();
        DockWidget.__Init();
        ToolBar.__Init();
        StatusBar.__Init();
        TabWidget.__Init();
        MainWindow.__Init();
        AbstractButton.__Init();
        MessageBox.__Init();
        TextOption.__Init();
        PlainTextEdit.__Init();
        ProgressBar.__Init();
        PushButton.__Init();
        RadioButton.__Init();
        ScrollArea.__Init();
        AbstractSlider.__Init();
        Slider.__Init();
        AbstractProxyModel.__Init();
        RegularExpression.__Init();
        SortFilterProxyModel.__Init();
        StyleOption.__Init();
        StyleOptionViewItem.__Init();
        StyledItemDelegate.__Init();
        TabBar.__Init();
        Timer.__Init();
        TreeView.__Init();
    }

    public static void Shutdown()
    {
        // module static shutdowns (if any, might be empty)
        TreeView.__Shutdown();
        Timer.__Shutdown();
        TabBar.__Shutdown();
        StyledItemDelegate.__Shutdown();
        StyleOptionViewItem.__Shutdown();
        StyleOption.__Shutdown();
        SortFilterProxyModel.__Shutdown();
        RegularExpression.__Shutdown();
        AbstractProxyModel.__Shutdown();
        Slider.__Shutdown();
        AbstractSlider.__Shutdown();
        ScrollArea.__Shutdown();
        RadioButton.__Shutdown();
        PushButton.__Shutdown();
        ProgressBar.__Shutdown();
        PlainTextEdit.__Shutdown();
        TextOption.__Shutdown();
        MessageBox.__Shutdown();
        AbstractButton.__Shutdown();
        MainWindow.__Shutdown();
        TabWidget.__Shutdown();
        StatusBar.__Shutdown();
        ToolBar.__Shutdown();
        DockWidget.__Shutdown();
        MenuBar.__Shutdown();
        Menu.__Shutdown();
        ListView.__Shutdown();
        AbstractItemView.__Shutdown();
        AbstractItemDelegate.__Shutdown();
        AbstractScrollArea.__Shutdown();
        AbstractListModel.__Shutdown();
        LineEdit.__Shutdown();
        Label.__Shutdown();
        Frame.__Shutdown();
        GroupBox.__Shutdown();
        GridLayout.__Shutdown();
        FileDialog.__Shutdown();
        Dialog.__Shutdown();
        ComboBox.__Shutdown();
        AbstractItemModel.__Shutdown();
        PersistentModelIndex.__Shutdown();
        ModelIndex.__Shutdown();
        Variant.__Shutdown();
        CalendarWidget.__Shutdown();
        Date.__Shutdown();
        BoxLayout.__Shutdown();
        Widget.__Shutdown();
        SizePolicy.__Shutdown();
        Cursor.__Shutdown();
        Region.__Shutdown();
        Action.__Shutdown();
        KeySequence.__Shutdown();
        Icon.__Shutdown();
        Painter.__Shutdown();
        Pixmap.__Shutdown();
        Image.__Shutdown();
        PaintDevice.__Shutdown();
        Enums.__Shutdown();
        PaintResources.__Shutdown();
        Color.__Shutdown();
        Layout.__Shutdown();
        Object.__Shutdown();
        Common.__Shutdown();
        Application.__Shutdown();
        // bye
        NativeImplClient.Shutdown();
    }

    public static void DumpTables()
    {
        NativeImplClient.DumpTables();
    }
}
