//push constant 4000
@4000
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
//push constant 5000
@5000
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
//pop Temp 1
@SP
M=M-1
A=M
D=M
@6
M=D
@SP
//Label LOOP
(Sys_LOOP)
//Goto LOOP
@Sys_LOOP
0;JMP
//push constant 4001
@4001
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
//push constant 5001
@5001
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
//push constant 200
@200
D=A
@SP
A=M
M=D
@SP
M=M+1
//pop Local 1
@SP
M=M-1
A=M
D=M
@301
M=D
@SP
//push constant 40
@40
D=A
@SP
A=M
M=D
@SP
M=M+1
//pop Local 2
@SP
M=M-1
A=M
D=M
@302
M=D
@SP
//push constant 6
@6
D=A
@SP
A=M
M=D
@SP
M=M+1
//pop Local 3
@SP
M=M-1
A=M
D=M
@303
M=D
@SP
//push constant 123
@123
D=A
@SP
A=M
M=D
@SP
M=M+1
//pop Temp 0
@SP
M=M-1
A=M
D=M
@5
M=D
@SP
//push Local 0
@0
D=A
@LCL
A=M
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//push Local 1
@1
D=A
@LCL
A=M
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//push Local 2
@2
D=A
@LCL
A=M
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//push Local 3
@3
D=A
@LCL
A=M
A=D+A
D=M
@SP
A=M
M=D
@SP
M=M+1
//push Local 4
@4
D=A
@LCL
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
//push constant 4002
@4002
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
//push constant 5002
@5002
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
//push constant 12
@12
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
