using Merchandise.Domain.Extensions;

namespace Merchandise.Domain.Services
{
    public static class Uuidv1Generator
    {
        private static readonly DateTime UUID_EPOCH = new DateTime(1582, 10, 15, 0, 0, 0, DateTimeKind.Utc);

        public static Guid GenerateTimeBasedGuid()
        {
            var timestamp = DateTimeExtensions.ToPhilippineTime(DateTime.UtcNow);
            var ticks = (timestamp - UUID_EPOCH).Ticks;

            byte[] guidBytes = new byte[16];

            Array.Copy(BitConverter.GetBytes((uint)(ticks & 0xFFFFFFFF)).Reverse().ToArray(), 0, guidBytes, 0, 4);
            Array.Copy(BitConverter.GetBytes((ushort)((ticks >> 32) & 0xFFFF)).Reverse().ToArray(), 0, guidBytes, 4, 2);

            ushort timeHi = (ushort)((ticks >> 48) & 0x0FFF);
            timeHi |= (ushort)(1 << 12);
            Array.Copy(BitConverter.GetBytes(timeHi).Reverse().ToArray(), 0, guidBytes, 6, 2);

            var random = new Random();
            ushort clockSeq = (ushort)random.Next(0, 16383);
            clockSeq |= 0x8000;
            Array.Copy(BitConverter.GetBytes(clockSeq).Reverse().ToArray(), 0, guidBytes, 8, 2);

            byte[] node = new byte[6];
            random.NextBytes(node);
            Array.Copy(node, 0, guidBytes, 10, 6);

            return new Guid(guidBytes);
        }

        public static DateTime GetDateFromUuidV1(Guid guid)
        {
            byte[] bytes = guid.ToByteArray();

            var timeLow = BitConverter.ToUInt32(bytes.Take(4).Reverse().ToArray(), 0);
            var timeMid = BitConverter.ToUInt16(bytes.Skip(4).Take(2).Reverse().ToArray(), 0);
            var timeHigh = BitConverter.ToUInt16(bytes.Skip(6).Take(2).Reverse().ToArray(), 0);

            long timestamp = ((long)(timeHigh & 0x0FFF) << 48)
                           | ((long)timeMid << 32)
                           | timeLow;

            DateTime uuidEpoch = new DateTime(1582, 10, 15, 0, 0, 0, DateTimeKind.Utc);
            long ticksSinceUuidEpoch = timestamp * 100;
            return uuidEpoch.AddTicks(ticksSinceUuidEpoch);
        }
    }
}
