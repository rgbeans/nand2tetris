//push constant 111
@111
D=A
@SP
A=M
M=D
@SP
M=M+1
//push constant 333
@333
D=A
@SP
A=M
M=D
@SP
M=M+1
//push constant 888
@888
D=A
@SP
A=M
M=D
@SP
M=M+1
//pop Static 8
@SP
M=M-1
A=M
D=M
@StaticTest.8
M=D
@SP
//pop Static 3
@SP
M=M-1
A=M
D=M
@StaticTest.3
M=D
@SP
//pop Static 1
@SP
M=M-1
A=M
D=M
@StaticTest.1
M=D
@SP
//push static 3
@StaticTest.3
D=M
@SP
A=M
M=D
@SP
M=M+1
//push static 1
@StaticTest.1
D=M
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
//push static 8
@StaticTest.8
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
