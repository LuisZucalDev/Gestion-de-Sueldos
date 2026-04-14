# Mejoras Lógica + Diseño Moderno (WinForms Pro)

**Info Gathered:**
- Lógica OK: Calc con AFP/salud rates real (Cuprum 7%-Provida 13%), defaults $5k/h $7k/extra.
- Empleado/Resultado serial OK.
- UI: ModernButton hover, BaseForm gradient/logout.
- Git main clean.

**Plan Detallado:**
1. **SalaryService:** +Cotización obligatoria (7%), impuesto renta auto, gratif anual preview.
2. **BaseForm:** Loading spinner, toast notifications, dark/light toggle.
3. **Forms:**
   - Admin: DataGrid dark theme, search/filter RUT/nombre, export Excel/CSV.
   - Calc: Input empleado RUT (auto-fill valorHora), chart breakdown %, print PDF.
   - List: Sort columns, stats avg sueldo.
4. **ModernButton:** Pulse anim, icons Segoe MDL2.
5. **AppTheme:** Light mode toggle.

**Depend Files:**
- Services/SalaryCalculatorService.cs
- Forms/* (inherit BaseForm)
- UI/AppTheme.cs, ModernButton.cs
- Repos add export.

**Followup:**
- Rebuild bin/Debug.
- Test calc 160h Modelo/Fonasa → $684k líquido.
- Git commit/push main.

Aprobar plan?
