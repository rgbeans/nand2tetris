//push Argument 1
@1
D=A
@ARG
A=M
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//pop Pointer 1
@SP
M=M-1
A=M
D=M
@4
M=D
@SP
//push constant 0
@0
D=A
@SP
A=M
M=D
@SP
M=M+1
//pop That 0
@0
D=A
@SP
M=M-1
A=M
D=M
@foo
M=D
@0
D=A
@THAT
A=D+M
D=A
@bar
M=D
@foo
D=M
@bar
A=M
M=D
//push constant 1
@1
D=A
@SP
A=M
M=D
@SP
M=M+1
//pop That 1
@1
D=A
@SP
M=M-1
A=M
D=M
@foo
M=D
@1
D=A
@THAT
A=D+M
D=A
@bar
M=D
@foo
D=M
@bar
A=M
M=D
//push Argument 0
@0
D=A
@ARG
A=M
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//push constant 2
@2
D=A
@SP
A=M
M=D
@SP
M=M+1
//Sub
@SP
A=M
A=A-1
D=M
D=-D
@SP
M=M-1
M=M-1
A=M
M=D+M
@SP
M=M+1
//pop Argument 0
@SP
M=M-1
A=M
D=M
@400
M=D
@SP
//Label MAIN_LOOP_START
(FibonacciSeries_MAIN_LOOP_START)
//push Argument 0
@0
D=A
@ARG
A=M
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//IfGoto COMPUTE_ELEMENT
@SP
M=M-1
A=M
D=M
@FibonacciSeries_COMPUTE_ELEMENT
D;JNE
//Goto END_PROGRAM
@FibonacciSeries_END_PROGRAM
0;JMP
//Label COMPUTE_ELEMENT
(FibonacciSeries_COMPUTE_ELEMENT)
//push That 0
@0
D=A
@THAT
A=M
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//push That 1
@1
D=A
@THAT
A=M
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//Add
@SP
A=M
A=A-1
D=M
@SP
M=M-1
M=M-1
A=M
M=D+M
@SP
M=M+1
//pop That 2
@2
D=A
@SP
M=M-1
A=M
D=M
@foo
M=D
@2
D=A
@THAT
A=D+M
D=A
@bar
M=D
@foo
D=M
@bar
A=M
M=D
@4
D=M
@SP
A=M
M=D
@SP
M=M+1
//push constant 1
@1
D=A
@SP
A=M
M=D
@SP
M=M+1
//Add
@SP
A=M
A=A-1
D=M
@SP
M=M-1
M=M-1
A=M
M=D+M
@SP
M=M+1
//pop Pointer 1
@SP
M=M-1
A=M
D=M
@4
M=D
@SP
//push Argument 0
@0
D=A
@ARG
A=M
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//push constant 1
@1
D=A
@SP
A=M
M=D
@SP
M=M+1
//Sub
@SP
A=M
A=A-1
D=M
D=-D
@SP
M=M-1
M=M-1
A=M
M=D+M
@SP
M=M+1
//pop Argument 0
@SP
M=M-1
A=M
D=M
@400
M=D
@SP
//Goto MAIN_LOOP_START
@FibonacciSeries_MAIN_LOOP_START
0;JMP
//Label END_PROGRAM
(FibonacciSeries_END_PROGRAM)