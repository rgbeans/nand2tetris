namespace VMTranslator
{
    //Where memorySegment is static, this, local, argument,
    // that, constant, pointer, or temp
    public enum MemorySegment
    {
        Static,
        This,
        Local,
        Argument,
        That,
        Constant,
        Pointer,
        Temp
    }
}