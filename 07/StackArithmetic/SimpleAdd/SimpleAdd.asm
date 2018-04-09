//push constant 7
@7
D=A
@SP
A=M
M=D
@SP
M=M+1
//push constant 8
@8
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
