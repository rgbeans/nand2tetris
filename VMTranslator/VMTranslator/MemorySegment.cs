namespace VMTranslator
{
    //Where memorySegment is static, this, local, argument,
    // that, constant, pointer, or temp
    public enum MemorySegment
    {
        Static,
        This,
        LCL,
        Arg,
        That,
        Constant,
        Pointer,
        Temp
    }
}