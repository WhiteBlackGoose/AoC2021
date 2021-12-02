# Codegen analysis report
*2021-12-02 21:09 UTC*
## Summary

| Job             | Method          | Branches  | Calls  | StaticStackAllocations  | CodegenSize  | ILSize  |
|:---------------:|:---------------:|:---------:|:------:|:-----------------------:|:------------:|:-------:|
| (Tier = Tier1)  | Int32 PartI()   | 22        | 1      | 88 B                    | 631 B        | 69 B    |
| (Tier = Tier1)  | Int32 PartII()  | 22        | 1      | 88 B                    | 642 B        | 69 B    |

## Codegens

### Int32 PartI(): Tier1
```assembly
00007FFBAB38CE80 4883EC58             sub       rsp,58h
00007FFBAB38CE84 C5F877               vzeroupper
00007FFBAB38CE87 C5D857E4             vxorps    xmm4,xmm4,xmm4
00007FFBAB38CE8B C5F97F642440         vmovdqa   [rsp+40h],xmm4
00007FFBAB38CE91 33C0                 xor       eax,eax
00007FFBAB38CE93 4889442450           mov       [rsp+50h],rax
00007FFBAB38CE98 488B4108             mov       rax,[rcx+8]
00007FFBAB38CE9C 4885C0               test      rax,rax
00007FFBAB38CE9F 7506                 jne       short 0000`7FFB`AB38`CEA7h
00007FFBAB38CEA1 33D2                 xor       edx,edx
00007FFBAB38CEA3 33C9                 xor       ecx,ecx
00007FFBAB38CEA5 EB07                 jmp       short 0000`7FFB`AB38`CEAEh
00007FFBAB38CEA7 488D500C             lea       rdx,[rax+0Ch]
00007FFBAB38CEAB 8B4808               mov       ecx,[rax+8]
00007FFBAB38CEAE 488D442440           lea       rax,[rsp+40h]
00007FFBAB38CEB3 488910               mov       [rax],rdx
00007FFBAB38CEB6 894808               mov       [rax+8],ecx
00007FFBAB38CEB9 33C0                 xor       eax,eax
00007FFBAB38CEBB 89442450             mov       [rsp+50h],eax
00007FFBAB38CEBF 33D2                 xor       edx,edx
00007FFBAB38CEC1 33C9                 xor       ecx,ecx
00007FFBAB38CEC3 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38CEC8 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB38CECD 0F8D16020000         jge       near 0000`7FFB`AB38`D0E9h
00007FFBAB38CED3 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38CED8 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB38CEDD 453B4108             cmp       r8d,[r9+8]
00007FFBAB38CEE1 0F830A020000         jae       near 0000`7FFB`AB38`D0F1h
00007FFBAB38CEE7 4D8B09               mov       r9,[r9]
00007FFBAB38CEEA 4D63C0               movsxd    r8,r8d
00007FFBAB38CEED 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB38CEF2 4183F864             cmp       r8d,64h
00007FFBAB38CEF6 0F84E6000000         je        near 0000`7FFB`AB38`CFE2h
00007FFBAB38CEFC 4183F866             cmp       r8d,66h
00007FFBAB38CF00 7450                 je        short 0000`7FFB`AB38`CF52h
00007FFBAB38CF02 4183F875             cmp       r8d,75h
00007FFBAB38CF06 0F859F010000         jne       near 0000`7FFB`AB38`D0ABh
00007FFBAB38CF0C 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38CF11 4183C003             add       r8d,3
00007FFBAB38CF15 4489442450           mov       [rsp+50h],r8d
00007FFBAB38CF1A 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38CF1F 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB38CF24 453B4108             cmp       r8d,[r9+8]
00007FFBAB38CF28 0F83C3010000         jae       near 0000`7FFB`AB38`D0F1h
00007FFBAB38CF2E 4D8B09               mov       r9,[r9]
00007FFBAB38CF31 4D63C0               movsxd    r8,r8d
00007FFBAB38CF34 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB38CF39 4183C0D0             add       r8d,0`FFFF`FFD0h
00007FFBAB38CF3D 448B4C2450           mov       r9d,[rsp+50h]
00007FFBAB38CF42 41FFC1               inc       r9d
00007FFBAB38CF45 44894C2450           mov       [rsp+50h],r9d
00007FFBAB38CF4A 412BD0               sub       edx,r8d
00007FFBAB38CF4D E926010000           jmp       0000`7FFB`AB38`D078h
00007FFBAB38CF52 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38CF57 4183C008             add       r8d,8
00007FFBAB38CF5B 4489442450           mov       [rsp+50h],r8d
00007FFBAB38CF60 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38CF65 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB38CF6A 453B4108             cmp       r8d,[r9+8]
00007FFBAB38CF6E 0F837D010000         jae       near 0000`7FFB`AB38`D0F1h
00007FFBAB38CF74 4D8B09               mov       r9,[r9]
00007FFBAB38CF77 4D63C0               movsxd    r8,r8d
00007FFBAB38CF7A 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB38CF7F 4183C0D0             add       r8d,0`FFFF`FFD0h
00007FFBAB38CF83 448B4C2450           mov       r9d,[rsp+50h]
00007FFBAB38CF88 41FFC1               inc       r9d
00007FFBAB38CF8B 44894C2450           mov       [rsp+50h],r9d
00007FFBAB38CF90 4103C0               add       eax,r8d
00007FFBAB38CF93 EB0D                 jmp       short 0000`7FFB`AB38`CFA2h
00007FFBAB38CF95 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38CF9A 41FFC0               inc       r8d
00007FFBAB38CF9D 4489442450           mov       [rsp+50h],r8d
00007FFBAB38CFA2 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38CFA7 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB38CFAC 0F8DF9000000         jge       near 0000`7FFB`AB38`D0ABh
00007FFBAB38CFB2 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38CFB7 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB38CFBC 453B4108             cmp       r8d,[r9+8]
00007FFBAB38CFC0 0F832B010000         jae       near 0000`7FFB`AB38`D0F1h
00007FFBAB38CFC6 4D8B09               mov       r9,[r9]
00007FFBAB38CFC9 4D63C0               movsxd    r8,r8d
00007FFBAB38CFCC 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB38CFD1 4183F861             cmp       r8d,61h
00007FFBAB38CFD5 7CBE                 jl        short 0000`7FFB`AB38`CF95h
00007FFBAB38CFD7 4183F87A             cmp       r8d,7Ah
00007FFBAB38CFDB 7FB8                 jg        short 0000`7FFB`AB38`CF95h
00007FFBAB38CFDD E9C9000000           jmp       0000`7FFB`AB38`D0ABh
00007FFBAB38CFE2 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38CFE7 4183C005             add       r8d,5
00007FFBAB38CFEB 4489442450           mov       [rsp+50h],r8d
00007FFBAB38CFF0 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38CFF5 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB38CFFA 453B4108             cmp       r8d,[r9+8]
00007FFBAB38CFFE 0F83ED000000         jae       near 0000`7FFB`AB38`D0F1h
00007FFBAB38D004 4D8B09               mov       r9,[r9]
00007FFBAB38D007 4D63C0               movsxd    r8,r8d
00007FFBAB38D00A 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB38D00F 4183C0D0             add       r8d,0`FFFF`FFD0h
00007FFBAB38D013 448B4C2450           mov       r9d,[rsp+50h]
00007FFBAB38D018 41FFC1               inc       r9d
00007FFBAB38D01B 44894C2450           mov       [rsp+50h],r9d
00007FFBAB38D020 4103D0               add       edx,r8d
00007FFBAB38D023 EB0D                 jmp       short 0000`7FFB`AB38`D032h
00007FFBAB38D025 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38D02A 41FFC0               inc       r8d
00007FFBAB38D02D 4489442450           mov       [rsp+50h],r8d
00007FFBAB38D032 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38D037 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB38D03C 7D6D                 jge       short 0000`7FFB`AB38`D0ABh
00007FFBAB38D03E 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38D043 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB38D048 453B4108             cmp       r8d,[r9+8]
00007FFBAB38D04C 0F839F000000         jae       near 0000`7FFB`AB38`D0F1h
00007FFBAB38D052 4D8B09               mov       r9,[r9]
00007FFBAB38D055 4D63C0               movsxd    r8,r8d
00007FFBAB38D058 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB38D05D 4183F861             cmp       r8d,61h
00007FFBAB38D061 7CC2                 jl        short 0000`7FFB`AB38`D025h
00007FFBAB38D063 4183F87A             cmp       r8d,7Ah
00007FFBAB38D067 7FBC                 jg        short 0000`7FFB`AB38`D025h
00007FFBAB38D069 EB40                 jmp       short 0000`7FFB`AB38`D0ABh
00007FFBAB38D06B 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38D070 41FFC0               inc       r8d
00007FFBAB38D073 4489442450           mov       [rsp+50h],r8d
00007FFBAB38D078 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38D07D 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB38D082 7D27                 jge       short 0000`7FFB`AB38`D0ABh
00007FFBAB38D084 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38D089 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB38D08E 453B4108             cmp       r8d,[r9+8]
00007FFBAB38D092 735D                 jae       short 0000`7FFB`AB38`D0F1h
00007FFBAB38D094 4D8B09               mov       r9,[r9]
00007FFBAB38D097 4D63C0               movsxd    r8,r8d
00007FFBAB38D09A 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB38D09F 4183F861             cmp       r8d,61h
00007FFBAB38D0A3 7CC6                 jl        short 0000`7FFB`AB38`D06Bh
00007FFBAB38D0A5 4183F87A             cmp       r8d,7Ah
00007FFBAB38D0A9 7FC0                 jg        short 0000`7FFB`AB38`D06Bh
00007FFBAB38D0AB C5F857C0             vxorps    xmm0,xmm0,xmm0
00007FFBAB38D0AF C5F911442420         vmovupd   [rsp+20h],xmm0
00007FFBAB38D0B5 89442420             mov       [rsp+20h],eax
00007FFBAB38D0B9 89542424             mov       [rsp+24h],edx
00007FFBAB38D0BD 894C2428             mov       [rsp+28h],ecx
00007FFBAB38D0C1 C5F910442420         vmovupd   xmm0,[rsp+20h]
00007FFBAB38D0C7 C5F911442430         vmovupd   [rsp+30h],xmm0
00007FFBAB38D0CD 8B442430             mov       eax,[rsp+30h]
00007FFBAB38D0D1 8B542434             mov       edx,[rsp+34h]
00007FFBAB38D0D5 8B4C2438             mov       ecx,[rsp+38h]
00007FFBAB38D0D9 448B442450           mov       r8d,[rsp+50h]
00007FFBAB38D0DE 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB38D0E3 0F8CEAFDFFFF         jl        near 0000`7FFB`AB38`CED3h
00007FFBAB38D0E9 0FAFC2               imul      eax,edx
00007FFBAB38D0EC 4883C458             add       rsp,58h
00007FFBAB38D0F0 C3                   ret
00007FFBAB38D0F1 E85A16A65F           call      0000`7FFC`0ADE`E750h
00007FFBAB38D0F6 CC                   int3
```
### Int32 PartII(): Tier1
```assembly
00007FFBAB3A9440 4883EC58             sub       rsp,58h
00007FFBAB3A9444 C5F877               vzeroupper
00007FFBAB3A9447 C4E15857E4           vxorps    xmm4,xmm4,xmm4
00007FFBAB3A944C C4E1797F642440       vmovdqa   [rsp+40h],xmm4
00007FFBAB3A9453 33C0                 xor       eax,eax
00007FFBAB3A9455 4889442450           mov       [rsp+50h],rax
00007FFBAB3A945A 488B4108             mov       rax,[rcx+8]
00007FFBAB3A945E 4885C0               test      rax,rax
00007FFBAB3A9461 7506                 jne       short 0000`7FFB`AB3A`9469h
00007FFBAB3A9463 33D2                 xor       edx,edx
00007FFBAB3A9465 33C9                 xor       ecx,ecx
00007FFBAB3A9467 EB07                 jmp       short 0000`7FFB`AB3A`9470h
00007FFBAB3A9469 488D500C             lea       rdx,[rax+0Ch]
00007FFBAB3A946D 8B4808               mov       ecx,[rax+8]
00007FFBAB3A9470 488D442440           lea       rax,[rsp+40h]
00007FFBAB3A9475 488910               mov       [rax],rdx
00007FFBAB3A9478 894808               mov       [rax+8],ecx
00007FFBAB3A947B 33C0                 xor       eax,eax
00007FFBAB3A947D 89442450             mov       [rsp+50h],eax
00007FFBAB3A9481 33D2                 xor       edx,edx
00007FFBAB3A9483 33C9                 xor       ecx,ecx
00007FFBAB3A9485 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A948A 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB3A948F 0F8D1F020000         jge       near 0000`7FFB`AB3A`96B4h
00007FFBAB3A9495 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A949A 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3A949F 453B4108             cmp       r8d,[r9+8]
00007FFBAB3A94A3 0F8313020000         jae       near 0000`7FFB`AB3A`96BCh
00007FFBAB3A94A9 4D8B09               mov       r9,[r9]
00007FFBAB3A94AC 4D63C0               movsxd    r8,r8d
00007FFBAB3A94AF 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3A94B4 4183F864             cmp       r8d,64h
00007FFBAB3A94B8 0F84ED000000         je        near 0000`7FFB`AB3A`95ABh
00007FFBAB3A94BE 4183F866             cmp       r8d,66h
00007FFBAB3A94C2 7450                 je        short 0000`7FFB`AB3A`9514h
00007FFBAB3A94C4 4183F875             cmp       r8d,75h
00007FFBAB3A94C8 0F85A8010000         jne       near 0000`7FFB`AB3A`9676h
00007FFBAB3A94CE 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A94D3 4183C003             add       r8d,3
00007FFBAB3A94D7 4489442450           mov       [rsp+50h],r8d
00007FFBAB3A94DC 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A94E1 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3A94E6 453B4108             cmp       r8d,[r9+8]
00007FFBAB3A94EA 0F83CC010000         jae       near 0000`7FFB`AB3A`96BCh
00007FFBAB3A94F0 4D8B09               mov       r9,[r9]
00007FFBAB3A94F3 4D63C0               movsxd    r8,r8d
00007FFBAB3A94F6 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3A94FB 4183C0D0             add       r8d,0`FFFF`FFD0h
00007FFBAB3A94FF 448B4C2450           mov       r9d,[rsp+50h]
00007FFBAB3A9504 41FFC1               inc       r9d
00007FFBAB3A9507 44894C2450           mov       [rsp+50h],r9d
00007FFBAB3A950C 412BC8               sub       ecx,r8d
00007FFBAB3A950F E92F010000           jmp       0000`7FFB`AB3A`9643h
00007FFBAB3A9514 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A9519 4183C008             add       r8d,8
00007FFBAB3A951D 4489442450           mov       [rsp+50h],r8d
00007FFBAB3A9522 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A9527 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3A952C 453B4108             cmp       r8d,[r9+8]
00007FFBAB3A9530 0F8386010000         jae       near 0000`7FFB`AB3A`96BCh
00007FFBAB3A9536 4D8B09               mov       r9,[r9]
00007FFBAB3A9539 4D63C0               movsxd    r8,r8d
00007FFBAB3A953C 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3A9541 4183C0D0             add       r8d,0`FFFF`FFD0h
00007FFBAB3A9545 448B4C2450           mov       r9d,[rsp+50h]
00007FFBAB3A954A 41FFC1               inc       r9d
00007FFBAB3A954D 44894C2450           mov       [rsp+50h],r9d
00007FFBAB3A9552 4103C0               add       eax,r8d
00007FFBAB3A9555 440FAFC1             imul      r8d,ecx
00007FFBAB3A9559 4103D0               add       edx,r8d
00007FFBAB3A955C EB0D                 jmp       short 0000`7FFB`AB3A`956Bh
00007FFBAB3A955E 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A9563 41FFC0               inc       r8d
00007FFBAB3A9566 4489442450           mov       [rsp+50h],r8d
00007FFBAB3A956B 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A9570 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB3A9575 0F8DFB000000         jge       near 0000`7FFB`AB3A`9676h
00007FFBAB3A957B 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A9580 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3A9585 453B4108             cmp       r8d,[r9+8]
00007FFBAB3A9589 0F832D010000         jae       near 0000`7FFB`AB3A`96BCh
00007FFBAB3A958F 4D8B09               mov       r9,[r9]
00007FFBAB3A9592 4D63C0               movsxd    r8,r8d
00007FFBAB3A9595 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3A959A 4183F861             cmp       r8d,61h
00007FFBAB3A959E 7CBE                 jl        short 0000`7FFB`AB3A`955Eh
00007FFBAB3A95A0 4183F87A             cmp       r8d,7Ah
00007FFBAB3A95A4 7FB8                 jg        short 0000`7FFB`AB3A`955Eh
00007FFBAB3A95A6 E9CB000000           jmp       0000`7FFB`AB3A`9676h
00007FFBAB3A95AB 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A95B0 4183C005             add       r8d,5
00007FFBAB3A95B4 4489442450           mov       [rsp+50h],r8d
00007FFBAB3A95B9 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A95BE 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3A95C3 453B4108             cmp       r8d,[r9+8]
00007FFBAB3A95C7 0F83EF000000         jae       near 0000`7FFB`AB3A`96BCh
00007FFBAB3A95CD 4D8B09               mov       r9,[r9]
00007FFBAB3A95D0 4D63C0               movsxd    r8,r8d
00007FFBAB3A95D3 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3A95D8 4183C0D0             add       r8d,0`FFFF`FFD0h
00007FFBAB3A95DC 448B4C2450           mov       r9d,[rsp+50h]
00007FFBAB3A95E1 41FFC1               inc       r9d
00007FFBAB3A95E4 44894C2450           mov       [rsp+50h],r9d
00007FFBAB3A95E9 4103C8               add       ecx,r8d
00007FFBAB3A95EC EB0F                 jmp       short 0000`7FFB`AB3A`95FDh
00007FFBAB3A95EE 6690                 xchg      ax,ax
00007FFBAB3A95F0 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A95F5 41FFC0               inc       r8d
00007FFBAB3A95F8 4489442450           mov       [rsp+50h],r8d
00007FFBAB3A95FD 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A9602 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB3A9607 7D6D                 jge       short 0000`7FFB`AB3A`9676h
00007FFBAB3A9609 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A960E 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3A9613 453B4108             cmp       r8d,[r9+8]
00007FFBAB3A9617 0F839F000000         jae       near 0000`7FFB`AB3A`96BCh
00007FFBAB3A961D 4D8B09               mov       r9,[r9]
00007FFBAB3A9620 4D63C0               movsxd    r8,r8d
00007FFBAB3A9623 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3A9628 4183F861             cmp       r8d,61h
00007FFBAB3A962C 7CC2                 jl        short 0000`7FFB`AB3A`95F0h
00007FFBAB3A962E 4183F87A             cmp       r8d,7Ah
00007FFBAB3A9632 7FBC                 jg        short 0000`7FFB`AB3A`95F0h
00007FFBAB3A9634 EB40                 jmp       short 0000`7FFB`AB3A`9676h
00007FFBAB3A9636 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A963B 41FFC0               inc       r8d
00007FFBAB3A963E 4489442450           mov       [rsp+50h],r8d
00007FFBAB3A9643 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A9648 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB3A964D 7D27                 jge       short 0000`7FFB`AB3A`9676h
00007FFBAB3A964F 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A9654 4C8D4C2440           lea       r9,[rsp+40h]
00007FFBAB3A9659 453B4108             cmp       r8d,[r9+8]
00007FFBAB3A965D 735D                 jae       short 0000`7FFB`AB3A`96BCh
00007FFBAB3A965F 4D8B09               mov       r9,[r9]
00007FFBAB3A9662 4D63C0               movsxd    r8,r8d
00007FFBAB3A9665 470FB70441           movzx     r8d,word [r9+r8*2]
00007FFBAB3A966A 4183F861             cmp       r8d,61h
00007FFBAB3A966E 7CC6                 jl        short 0000`7FFB`AB3A`9636h
00007FFBAB3A9670 4183F87A             cmp       r8d,7Ah
00007FFBAB3A9674 7FC0                 jg        short 0000`7FFB`AB3A`9636h
00007FFBAB3A9676 C5F857C0             vxorps    xmm0,xmm0,xmm0
00007FFBAB3A967A C5F911442420         vmovupd   [rsp+20h],xmm0
00007FFBAB3A9680 89442420             mov       [rsp+20h],eax
00007FFBAB3A9684 89542424             mov       [rsp+24h],edx
00007FFBAB3A9688 894C2428             mov       [rsp+28h],ecx
00007FFBAB3A968C C5F910442420         vmovupd   xmm0,[rsp+20h]
00007FFBAB3A9692 C5F911442430         vmovupd   [rsp+30h],xmm0
00007FFBAB3A9698 8B442430             mov       eax,[rsp+30h]
00007FFBAB3A969C 8B542434             mov       edx,[rsp+34h]
00007FFBAB3A96A0 8B4C2438             mov       ecx,[rsp+38h]
00007FFBAB3A96A4 448B442450           mov       r8d,[rsp+50h]
00007FFBAB3A96A9 443B442448           cmp       r8d,[rsp+48h]
00007FFBAB3A96AE 0F8CE1FDFFFF         jl        near 0000`7FFB`AB3A`9495h
00007FFBAB3A96B4 0FAFC2               imul      eax,edx
00007FFBAB3A96B7 4883C458             add       rsp,58h
00007FFBAB3A96BB C3                   ret
00007FFBAB3A96BC E88F50A45F           call      0000`7FFC`0ADE`E750h
00007FFBAB3A96C1 CC                   int3
```
