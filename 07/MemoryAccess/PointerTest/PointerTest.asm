//push constant 3030
@3030
D=A
@SP
A=M
M=D
@SP
M=M+1
//pop Pointer 0
@SP
M=M-1
A=M
D=M
@3
M=D
@SP
//push constant 3040
@3040
D=A
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
//push constant 32
@32
D=A
@SP
A=M
M=D
@SP
M=M+1
//pop This 2
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
@THIS
A=D+M
D=A
@bar
M=D
@foo
D=M
@bar
A=M
M=D
//push constant 46
@46
D=A
@SP
A=M
M=D
@SP
M=M+1
//pop That 6
@6
D=A
@SP
M=M-1
A=M
D=M
@foo
M=D
@6
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
@3
D=M
@SP
A=M
M=D
@SP
M=M+1
@4
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
//push This 2
@2
D=A
@THIS
A=M
A=D+A
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
//push That 6
@6
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
