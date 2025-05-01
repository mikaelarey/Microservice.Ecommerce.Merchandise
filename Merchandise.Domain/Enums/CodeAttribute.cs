using System.ComponentModel;

namespace Merchandise.Domain.Enums
{
    public enum CodeAttribute
    {
        Size = 1,
        Color = 2,
        Fit = 3,
        Material = 4,
        Pattern = 5,
        Sleeve = 6,
        [Description("Neck Line")]
        NeckLine = 7,
        Hemline = 8,
        [Description("Closure Type")]
        ClosureType = 9,
        Stretchability = 10,
        Occasion = 11,
        Care = 12,
        Gender = 13,
        Season = 14,
        [Description("Model Number")]
        ModelNumber = 15,
        Dimensions = 17,
        Weight = 18,
        [Description("Screen Size")]
        ScreenSize = 19,
        Resolution = 20,
        [Description("Battery Capacity")]
        BatteryCapacity = 21,
        [Description("Battery Life")]
        BatteryLife = 22,
        [Description("Storage Capacity")]
        StorageCapacity = 23,
        Ram = 24,
        [Description("Processor Type")]
        ProcessorType = 25,
        [Description("Processor Speed")]
        ProcessorSpeed = 26,
        [Description("Operating System")]
        OperatingSystem = 27,
        Ports = 28,
        Connectivity = 29,
        [Description("Camera Specs")]
        CameraSpecs = 30,
        Warranty = 31,
        [Description("Energy Efficiency Rating")]
        EnergyEfficiencyRating = 32,
        [Description("Accessories Included")]
        AccessoriesIncluded = 33,
        Type = 34,
        [Description("Color Finish")]
        ColorFinish = 38,
        Style = 39,
        [Description("Number Of Seats")]
        NumberOfSeats = 40,
        [Description("Assembly Required")]
        AssemblyRequired = 41,
        [Description("Load Capacity")]
        LoadCapacity = 42,
        [Description("Upholstery Type")]
        UpholsteryType = 43,
        [Description("Storage Features")]
        StorageFeatures = 44,
        [Description("Mounting Type")]
        MountingType = 45,
        [Description("Care Instructions")]
        CareInstructions = 46,
        [Description("Room Type")]
        RoomType = 48,
    }
}
