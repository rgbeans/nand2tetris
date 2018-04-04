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
//pop Static 0
@SP
M=M-1
A=M
D=M
@Class1.0
M=D
@SP
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
//pop Static 1
@SP
M=M-1
A=M
D=M
@Class1.1
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
//push static 0
@Class1.0
D=M
@SP
A=M
M=D
@SP
M=M+1
//push static 1
@Class1.1
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
