<?php

namespace App\Livewire\Admins\Reports;

use App\Models\Report;
use Livewire\Component;

class AdminReportHistory extends Component
{
    public function render()
    {
        return view(
            'livewire.admins.reports.admin-report-history',
            [
                'reports' => Report::whereNot("report_response_id", 1)->orderBy("updated_at", "desc")->paginate(10),
            ]
        );
    }
}
