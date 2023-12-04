<?php

namespace App\Livewire\Admins\ReportTypes;

use App\Models\ReportType;
use Livewire\Component;

class AdminReportTypeShow extends Component
{
    public $report_type;
    public $name;
    public $description;
    public $model_type;
    public $can_use;
    public function mount($report_type_id)
    {
        $this->report_type = ReportType::findOrFail($report_type_id);
        $this->name = $this->report_type->name;
        $this->description = $this->report_type->description;
        $this->can_use = $this->report_type->can_use;
        $this->model_type = $this->report_type->model_type;
    }

    public function save()
    {
        $this->validate(
            [
                'name' => ["required", "min:1", 'max:64'],
                'description' => ["required", "min:3", 'max:512'],
            ],
            [
                'name.required' => "Cần nhập tên",
                'name.min' => "Cần nhập tên với tối thiểu :min ký tự",
                'name.max' => "Cần nhập tên với tối đa :max ký tự",

                'description.required' => "Cần nhập miêu tả",
                'description.min' => "Cần nhập miêu tả với tối thiểu :min ký tự",
                'description.max' => "Cần nhập miêu tả với tối đa :max ký tự",

            ]
        );
        $this->report_type->name = $this->name;
        $this->report_type->description = $this->description;
        $this->report_type->can_use = $this->can_use;

        $this->report_type->save();

        $this->redirect(route("admin.report-type.index"));
    }

    public function render()
    {
        return view('livewire.admins.report-types.admin-report-type-show');
    }
}
