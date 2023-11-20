<?php

namespace App\Livewire\Admins\SystemNotice;

use App\Models\SystemNotification;
use Livewire\WithPagination;
use Livewire\Component;

class AdminSystemNoticeIndex extends Component
{
    use WithPagination;
    public function render()
    {
        return view(
            'livewire.admins.system-notice.admin-system-notice-index',
            [
                'notifications' => SystemNotification::orderBy("can_use", "DESC")->paginate(10),
            ]
        );
    }
}
